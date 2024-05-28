﻿using Microsoft.AspNetCore.Http;

namespace SWPSolution.ViewModels.Catalog.Product.Manage
{
    public class ProductCreateRequest
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }

        public IFormFile ThumbnailImage { get; set; }
    }
}