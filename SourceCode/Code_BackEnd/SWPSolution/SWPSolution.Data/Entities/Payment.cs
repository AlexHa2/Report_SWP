﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SWPSolution.Data.Entities;

public partial class Payment
{
    public string? PaymentId { get; set; }

    public string OrderId { get; set; }

    public double Amount { get; set; }

    public double DiscountValue { get; set; }

    public bool PaymentStatus { get; set; }

    public string PaymentMethod { get; set; }

    public DateTime PaymentDate { get; set; }

    [JsonIgnore]
    public virtual Order Order { get; set; }
}