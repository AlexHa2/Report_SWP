﻿
using SWPSolution.ViewModels.Catalog.Product;
using SWPSolution.ViewModels.Common;

namespace SWPSolution.Application.Catalog.Product
{
    public interface IPublicProductService
    {
        Task<PageResult<ProductViewModel>> GetAllPaging(GetPublicProductPagingRequest request);
       
        Task<List<ProductViewModel>> GetAll();
    }
}
