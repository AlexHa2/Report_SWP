﻿
using SWPSolution.ViewModels.Catalog.Product;
using SWPSolution.ViewModels.Common;

namespace SWPSolution.Application.Catalog.Product
{
    public interface IPublicProductService
    {
        Task<int> GetTotalProductCountAsync();

        Task<PageResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request);

        Task<List<ProductViewModel>> GetAll();
    }
}
