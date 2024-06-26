﻿using Microsoft.AspNetCore.Http;

namespace SWPSolution.ViewModels.Catalog.Product
{
    public class ProductUpdateRequest
    {
        public string ProductId { get; set; }

        public string CategoriesId { get; set; }

        public string ProductName { get; set; }

        public int? Quantity { get; set; }

        public string Description { get; set; }

        public IFormFile ThumbnailImage { get; set; }
    }
}
