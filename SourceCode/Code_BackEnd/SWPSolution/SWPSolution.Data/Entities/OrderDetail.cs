﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SWPSolution.Data.Entities;

public partial class OrderDetail
{
    public string OrderdetailId { get; set; }

    public string ProductId { get; set; }

    public string OrderId { get; set; }

    public int? Quantity { get; set; }

    public virtual Order Order { get; set; }

    public virtual Product Product { get; set; }
}