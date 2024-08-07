﻿using Microsoft.AspNetCore.Http;
using SWPSolution.Data.Enum;

namespace SWPSolution.ViewModels.Catalog.Product
{
    public class ProductCreateRequest
    {
        public string ProductName { get; set; }

        public string CategoryId { get; set; }

        public int Quantity { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }

        public ProductStatus StatusDescription { get; set; }

        public IFormFile? ThumbnailImage { get; set; }
    }
}
