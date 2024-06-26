﻿using System.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWPSolution.Application.AppPayment;
using SWPSolution.Application.AppPayment.VNPay;
using SWPSolution.Application.Sales;
using SWPSolution.Application.Session;
using SWPSolution.Application.Catalog.Promotion;
using SWPSolution.Data.Entities;
using SWPSolution.Utilities.Exceptions;
using SWPSolution.ViewModels.Payment;
using SWPSolution.ViewModels.Sales;
using System.Threading.Tasks;
using SWPSolution.ViewModels.System.Users;

namespace SWPSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IVnPayService _vnPayService;
        private readonly ISessionService _sessionService;
        private readonly IPaymentService _paymentService;
        private readonly IPromotionService _promotionService;
        private readonly SWPSolutionDBContext _context;

        public OrderController(
            IOrderService orderService,
            IConfiguration config,
            IVnPayService vnPayService,
            ISessionService sessionService,
            IPaymentService paymentService,
            IPromotionService promotionService,
            SWPSolutionDBContext context)
        {
            _orderService = orderService;
            _vnPayService = vnPayService;
            _sessionService = sessionService;
            _paymentService = paymentService;
            _promotionService = promotionService;
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _orderService.PlaceOrderAsync(request);
            if (!result.Success)
            {
                return BadRequest(new { error = result.ErrorMessage });
            }

            var discountValue = await _promotionService.GetDiscountValueByPromotionId(request.PromotionId) ?? 0;

            var payment = new PaymentRequest
            {
                OrderId = result.Order.OrderId,
                Amount = result.Order.TotalAmount,
                DiscountValue = discountValue,
                PaymentStatus = false,
                PaymentMethod = ""
            };
            var paymentResult = await _paymentService.Create(payment);
            if (paymentResult == null)
            {
                return BadRequest(new { message = "Cannot create payment" });
            }

            return Ok(new
            {
                Order = result.Order,
                Payment = paymentResult
            });
        }

        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAll();
            if (orders == null)
            {
                return NotFound(new { message = "No orders were found" });
            }
            return Ok(orders);
        }

        [HttpGet("GetOrderById/{orderId}")]
        public async Task<IActionResult> GetOrderById(string orderId)
        {
            var order = await _orderService.GetOrderById(orderId);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpGet("member/{memberId}")]
        public IActionResult GetOrdersByMemberId(string memberId)
        {
            var orders = _orderService.GetOrdersByMemberId(memberId);
            return Ok(orders);
        }

        [HttpPut("{orderId}/status")]
        public async Task<IActionResult> UpdateOrderStatus(string orderId, [FromBody] UpdateOrderStatusRequest request)
        {
            await _orderService.UpdateOrderStatus(orderId, request.NewStatus);
            return NoContent();
        }

        [HttpPost("cancel/{orderId}")]
        public async Task<IActionResult> CancelOrder(string orderId)
        {
            try
            {
                var result = await _orderService.CancelOrderAsync(orderId);
                return Ok(new { message = result });
            }
            catch (SWPException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("CheckoutVNPay")]
        public async Task<IActionResult> CheckoutVNPay([FromBody] OrderVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var payment = _context.Payments
                .FirstOrDefault(p => p.OrderId == model.OrderId && p.Amount == model.TotalAmount);
            if (payment == null)
            {
                throw new Exception("Payment not found");
            }
            string paymentId = payment.PaymentId;

            var vnPayModel = new VnPaymentRequestModel
            {
                Amount = model.TotalAmount,
                CreatedDate = model.OrderDate,
                Description = $"{model.MemberId}",
                FullName = model.MemberId,
                OrderId = model.OrderId,
                PaymentId = paymentId
            };

            var paymentRequest = new PaymentRequest
            {
                OrderId = model.OrderId,
                Amount = model.TotalAmount,
                PaymentMethod = "VNPay",
                PaymentStatus = false,
                PaymentDate = DateTime.UtcNow
            };

            var existingPayment = _context.Payments.FirstOrDefault(p => p.PaymentId == paymentId);
            if (existingPayment != null)
            {
                await _paymentService.Update(paymentId, paymentRequest);
            }
            else
            {
                await _paymentService.Create(paymentRequest);
            }

            var paymentUrl = _vnPayService.CreatePaymentUrl(HttpContext, vnPayModel);

            return Ok(new { PaymentUrl = paymentUrl });
        }

        [HttpGet("PaymentCallBack")]
        public async Task<IActionResult> PaymentCallBack()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            if (response == null || response.VnPayResponseCode != "00")
            {
                return BadRequest(new { Message = "Payment failed" });
            }

            var orderId = response.PaymentId;
            var payment = _context.Payments.FirstOrDefault(p => p.OrderId == orderId);

            if (payment == null)
            {
                return BadRequest(new { Message = $"Payment with id {orderId} not found" });
            }

            payment.PaymentStatus = true;

            var order = await _context.Orders.FindAsync(payment.OrderId);
            if (order == null)
            {
                return BadRequest(new { Message = $"Order with id {payment.OrderId} not found" });
            }

            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();

            await _orderService.SendReceiptEmailAsync(order.MemberId, order);

            return Ok(new { Message = "Payment status updated successfully" });
        }
    }
}
