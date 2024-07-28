﻿using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using SWPSolution.ViewModels.Catalog.Blog;
using SWPSolution.ViewModels.Catalog.Product;
using SWPSolution.ViewModels.Common;
using SWPSolution.ViewModels.Sales;
using SWPSolution.ViewModels.System.Users;

namespace SWPSolution.AdminApp.Services
{
    public interface IUserApiClient
    {
        public Task<string> Authenticate(LoginRequest request);

        Task<PageResult<MemberInfoVM>> GetUsersPagings(GetUserPagingRequest request);

        Task<ApiResult<MemberInfoVM>> GetUserById(string id);

        Task<PageResult<StaffInfoVM>> GetStaffsPagings(GetUserPagingRequest request);

        Task<ApiResult<StaffInfoVM>> GetStaffById(string id);

        Task<PageResult<ProductViewModel>> GetProductsNamePagings(GetUserPagingRequest request);

        Task<PageResult<ProductViewModel>> GetProductsCatePagings(GetUserPagingRequest request);

        Task<ApiResult<ProductViewModel>> GetProductById(string id);

        Task<PageResult<ReviewVM>> GetReviewsPagings(GetUserPagingRequest request);

        Task<ApiResult<ReviewVM>> GetReviewById(string id);

        Task<PageResult<BlogDetailVM>> GetBlogsPagings(GetUserPagingRequest request);

        Task<ApiResult<BlogDetailVM>> GetBlogById(string id);

        Task<PageResult<OrderTrackingVM>> GetOrderTrackingsPagings(GetTrackingPagingRequest request);

        Task<PageResult<OrderTrackingVM>> GetPreorderTrackingsPagings(GetTrackingPagingRequest request);
    }
}
