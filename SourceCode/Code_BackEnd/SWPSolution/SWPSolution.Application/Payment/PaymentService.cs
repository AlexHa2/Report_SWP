﻿
using SWPSolution.ViewModels.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWPSolution.Data.Entities;
using SWPSolution.Utilities.Exceptions;
using System.Data.Entity;

namespace SWPSolution.Application.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly SWPSolutionDBContext _context;

        public PaymentService(SWPSolutionDBContext context) 
        {
            _context = context;
        }
        public async Task<string> Create(PaymentRequest request)
        {
            var payment = new Data.Entities.Payment
            {
                PaymentId = GeneratePaymentId(),
                OrderId = request.OrderId,
                Amount = request.Amount,
                DiscountValue = request.DiscountValue,
                PaymentStatus   = request.PaymentStatus,
                PaymentMethod = request.PaymentMethod,
                PaymentDate = request.PaymentDate,
            };
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            var insertedPayment = _context.Payments.FirstOrDefault(p => p.OrderId == request.OrderId);
            return insertedPayment.PaymentId;
        }

        public async Task<bool> Delete(string id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return false;

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<PaymentVM>> GetAll()
        {
            return await _context.Payments
                .Select(p => new PaymentVM
                {
                    PaymentId = p.PaymentId,    
                    OrderId = p.OrderId,
                    Amount = p.Amount,
                    DiscountValue = p.DiscountValue,
                    PaymentStatus = p.PaymentStatus,
                    PaymentMethod = p.PaymentMethod,
                    PaymentDate = p.PaymentDate,
                })
                .ToListAsync();
        }

        public async Task<PaymentRequest> GetById(string id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return null;

            return new PaymentRequest
            {
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                DiscountValue = payment.DiscountValue,
                PaymentStatus = payment.PaymentStatus,
                PaymentMethod = payment.PaymentMethod,
                PaymentDate = payment.PaymentDate,
            };
        }

        public async Task<int> Update(string id, PaymentRequest request)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) throw new SWPException($"Cannot find payment with id: {id}");

            
            payment.Amount = request.Amount;
            payment.DiscountValue = request.DiscountValue;
            payment.PaymentStatus = request.PaymentStatus;
            payment.PaymentMethod = request.PaymentMethod;
            payment.PaymentDate = request.PaymentDate;
            payment.OrderId = request.OrderId;

            _context.Payments.Update(payment);
            return await _context.SaveChangesAsync();
        }


        //Generate id
        private string GeneratePaymentId()
        {
            // Generate categories_ID based on current month, year, and auto-increment
            string month = DateTime.Now.ToString("MM");
            string year = DateTime.Now.ToString("yy");

            int autoIncrement = GetNextAutoIncrement(month, year);

            string formattedAutoIncrement = autoIncrement.ToString().PadLeft(3, '0');

            return $"PM{month}{year}{formattedAutoIncrement}";
        }

        private int GetNextAutoIncrement(string month, string year)
        {
            // Generate the pattern for categories_ID to match in SQL query
            string pattern = $"PM{month}{year}";

            // Retrieve the maximum auto-increment value from existing categories for the given month and year
            var maxAutoIncrement = _context.Payments
                .Where(c => c.PaymentId.StartsWith(pattern))
                .Select(c => c.PaymentId.Substring(6, 3)) // Select substring of auto-increment part
                .AsEnumerable() // Switch to client evaluation from this point
                .Select(s => int.Parse(s)) // Parse string to int
                .DefaultIfEmpty(0)
                .Max();

            return maxAutoIncrement + 1;
        }
    }
}
