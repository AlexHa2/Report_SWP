﻿
using SWPSolution.Data.Entities;
using SWPSolution.ViewModels.Common;
using SWPSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SWPSolution.Application.System.User
{
    public interface IUserService
    {
        Task<string> Authenticate(LoginRequest request);

        Task<object> HandleGoogleLoginAsync(GoogleLoginRequest request);

        Task<bool> Register(RegisterRequest request);

        Task<bool> ForgotPassword([Required]string email);

        Task<bool> ResetPassword(ResetPasswordVM model);

        Task<bool> ConfirmEmail(string otp);

        Task<bool> TestEmail(string emailAddress);

        Task<List<MemberInfoVM>> GetAllMembersAsync();

        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);

        Task<MemberInfoVM> GetMemberByIdAsync(string memberId);

        Task<bool> UpdateMemberAsync(string memberId, UpdateMemberRequest request);

        Task<List<MemberAddressVM>> GetMemberAddressById(string memberId);

        Task<List<MemberAddressVM>> GetAllAddresses();

        Task<bool> UpdateMemberAddress(string memberId, UpdateAddressRequest request);

        Task<bool> AddMemberAddressAsync(string memberId, AddAddressRequest request);

        Task<bool> DeleteUserAsync(string memberId);

        Task<bool> DeleteMemberAddress(string id);

       Task<string> ExtractMemberIdFromTokenAsync(string token);

        ClaimsPrincipal ValidateToken(string jwtToken);

        Task<StaffInfoVM> GetStaffById(string staffId);

        Task<List<StaffInfoVM>> GetAllStaffs();

        Task<bool> UpdateStaff(string id, UpdateStaffRequest request);

        Task<bool> DeleteStaff(string staffId);

        Task<bool> RegisterStaff(List<RegisterRequest> requests);

        Task<string> AuthenticateStaff(LoginRequest request);

        Task<string> ExtractStaffIdFromTokenAsync(string token);

        Task<PageResult<UserVm>> GetUsersPaging(GetUserPagingRequest request);

        Task<ApiResult<UserVm>> GetById(Guid id);

        Task<bool> BuyGiftWithPointsAsync(string memberId, int giftId);

    }
}
