﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWPSolution.ViewModels.Catalog.Categories;
using SWPSolution.ViewModels.Catalog.Product;
using SWPSolution.ViewModels.Common;

namespace SWPSolution.Application.Catalog.Categories
{
    public interface ICategoryService
    {
        Task<bool> Create(CategoryCreateRequest request);
        Task<bool> CreateMultiple(List<CategoryCreateRequest> requests);
        Task<bool> Update(string id, CategoryUpdateRequest request);
        Task<bool> Delete(string id);
        Task<CategoriesVM> GetById(string id);
        Task<List<CategoriesVM>> GetByName(string search);
        Task<List<CategoriesVM>> GetAll();
        Task<int> GetTotalCategoryCountAsync();

        Task<PageResult<CategoriesVM>> GetAllPaging(CategoryPagingRequest request);

    }
}
