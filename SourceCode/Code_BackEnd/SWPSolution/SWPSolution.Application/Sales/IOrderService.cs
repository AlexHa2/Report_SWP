﻿using SWPSolution.ViewModels.Sales;
using SWPSolution.Data.Entities;
using SWPSolution.ViewModels.System.Users;
using static SWPSolution.Application.Sales.OrderService;
using SWPSolution.Data.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWPSolution.Application.Sales
{
    public interface IOrderService
    {
        // Order creation and processing
        Task<Order> CreateOrder(OrderRequest orderRequest);
        Task<PlaceOrderResult> PlaceOrderAsync(OrderRequest orderRequest);

        // Order retrieval
        Task<List<Order>> GetAll();
        Task<OrderVM> GetOrderById(string orderId);
        IEnumerable<Order> GetOrdersByMemberId(string memberId);

        // Order status management
        Task<string> UpdateOrderStatus(string orderId, OrderStatus newStatus);
        Task<string> CancelOrderAsync(string orderId);

        // Promotion and discount management
        

        // Utility methods
        Task<string> ExtractMemberIdFromTokenAsync(string token);
        Task SendReceiptEmailAsync(string recipientEmail, Order order);
    }
}
