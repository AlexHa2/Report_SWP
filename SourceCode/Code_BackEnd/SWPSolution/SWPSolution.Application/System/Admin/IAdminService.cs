﻿using Microsoft.AspNetCore.Mvc;
using SWPSolution.Data.Entities;
using SWPSolution.ViewModels.Catalog.Blog;
using SWPSolution.ViewModels.Catalog.Categories;
using SWPSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SWPSolution.Application.System.Admin
{
    public interface IAdminService
    {
        Task<string> AuthenticateAdmin(LoginRequest request);

        Task<string> ExtractAdminIdFromTokenAsync(string token);

        Task<bool> RegisterAdmin(RegisterRequest request);

        Task<StaffInfoVM> GetAdminById(string adminId);

        Task<bool> UpdateAdmin(string id, UpdateAdminRequest request);

        Task<bool> DeleteAdmin(string adminId);

        ClaimsPrincipal ValidateToken(string jwtToken);

        Task<bool> CreateBlogAsync(string staffId, BlogCreateRequest request);

        Task<List<Blog>> GetAllBlogsAsync();

        Task<BlogDetailVM> GetBlogByIdAsync(string staffId);

        Task<bool> UpdateBlogAsync(string staffId, UpdateBlogRequest request);

        Task<bool> DeleteBlogAsync(string blogId);

        Task<bool> AddOrder(string memberId, AddOrderRequest request);

        Task<bool> UpdateOrder(string id, OrderUpdateRequest request);

        Task<bool> DeleteOrder(string id);

        Task<OrderVM> GetOrderById(string id);

        Task<List<OrderVM>> GetAllOrder();
    }
}
