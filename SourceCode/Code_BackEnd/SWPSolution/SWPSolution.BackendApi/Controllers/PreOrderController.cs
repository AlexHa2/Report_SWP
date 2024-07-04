﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWPSolution.Application.AppPayment;
using SWPSolution.Application.AppPayment.VNPay;
using SWPSolution.Application.Sales;
using SWPSolution.Data.Entities;
using SWPSolution.Data.Enum;
using SWPSolution.ViewModels.Payment;
using SWPSolution.ViewModels.Sales;
using System.Data;
using System.Data.Entity;

namespace SWPSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreOrderController : ControllerBase
    {
        private readonly SWPSolutionDBContext _context;
        private readonly IPaymentService _paymentService;
        private readonly IVnPayService _vnPayService;
        private readonly IPreOrderService _preOrderService;

        public PreOrderController(
            SWPSolutionDBContext context,
            IPaymentService paymentService,
            IVnPayService vnPayService,
            IPreOrderService preOrderService
            ) 
        {
            _context = context;
            _paymentService = paymentService;
            _vnPayService = vnPayService;
            _preOrderService = preOrderService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreatePreOrder([FromBody] PreOrderVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var preOrder = await _preOrderService.CreatePreOrder(model.ProductId, model.MemberId, model.Quantity);
                return Ok(preOrder);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("availability")]
        public async Task<IActionResult> IsProductAvailable(string productId, int quantity)
        {
            var isAvailable = await _preOrderService.IsProductAvailable(productId, quantity);
            return Ok(isAvailable);
        }

        [HttpPost("process-deposit")]
        public async Task<IActionResult> ProcessPreOrderDeposit([FromBody] PreOrderVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var payment = await _preOrderService.ProcessPreOrderDeposit(model.PreorderId, model.Price);
                return Ok(payment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{preorderId}")]
        public async Task<IActionResult> GetPreOrder(string preorderId)
        {
            var preOrder = await _preOrderService.GetPreOrder(preorderId);
            if (preOrder == null)
            {
                return NotFound(new { message = "No preorders were found" });
            }

            return Ok(preOrder);
        }

        [HttpGet("GetAllPreOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _preOrderService.GetAll();
            if (orders == null)
            {
                return NotFound(new { message = "No orders were found" });
            }
            return Ok(orders);
        }

        [HttpPost("update-status")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] PreOrder model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _preOrderService.UpdateOrderStatus(model.PreorderId, model.Status);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("CheckoutVNPayPreOrder")]
        public async Task<IActionResult> CheckoutVNPayPreOrder([FromBody] PreOrderVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var preorder =  _context.PreOrders.FirstOrDefault(po => po.PreorderId == model.PreorderId && po.Price == model.Price);
            if (preorder == null)
            {
                throw new Exception("Preorder not found");
            }

            var payment = _context.Payments.FirstOrDefault(p => p.OrderId == model.PreorderId && p.Amount == model.Price);
            string paymentId = payment?.PaymentId;

            var vnPayModel = new VnPaymentRequestModel
            {
                Amount = model.Price,
                CreatedDate = model.PreorderDate,
                Description = $"{model.MemberId}",
                FullName = model.MemberId,
                OrderId = model.PreorderId,
                PaymentId = paymentId
            };

            var paymentRequest = new PaymentRequest
            {
                OrderId = model.PreorderId,
                Amount = model.Price,
                PaymentMethod = "VNPay",
                PaymentStatus = false,
                PaymentDate = DateTime.UtcNow
            };

            if (payment != null)
            {
                await _paymentService.Update(paymentId, paymentRequest);
            }
            else
            {
                await _paymentService.Create(paymentRequest);
            }

            var paymentUrl = _vnPayService.CreatePaymentUrl(HttpContext, vnPayModel);

            await _preOrderService.NotifyCustomer(preorder.MemberId, preorder, paymentUrl);

            return Ok(new { PaymentUrl = paymentUrl });
        }

    }
}
