﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SWPSolution.Data.Entities;

public partial class Order
{
    public string OrderId { get; set; }

    public string MemberId { get; set; }

    public string PromotionId { get; set; }

    public string ShippingAddress { get; set; }

    public double? TotalAmount { get; set; }

    public bool? OrderStatus { get; set; }

    public DateTime? OrderDate { get; set; }

    public virtual Member Member { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Promotion Promotion { get; set; }

    public AppUser AppUser { get; set; }
}