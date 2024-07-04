﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SWPSolution.Data.Entities;

public partial class Product
{
    public string ProductId { get; set; }

    public string CategoriesId { get; set; }

    public string ProductName { get; set; }

    public int? Quantity { get; set; }

    public double Price { get; set; }

    public string Description { get; set; }

    public string StatusDescription { get; set; }

    public string Image { get; set; }

    public virtual Category Categories { get; set; }


    [JsonIgnore]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    [JsonIgnore]
    public virtual ICollection<PreOrder> PreOrders { get; set; } = new List<PreOrder>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}