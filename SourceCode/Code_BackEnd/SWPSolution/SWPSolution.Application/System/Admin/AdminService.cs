﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using SWPSolution.Application.System.User;
using SWPSolution.Data.Entities;
using SWPSolution.Utilities.Exceptions;
using SWPSolution.ViewModels.Catalog.Blog;
using SWPSolution.ViewModels.Catalog.Categories;
using SWPSolution.ViewModels.Common;
using SWPSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SWPSolution.Application.System.Admin
{
    
    public class AdminService : IAdminService
    {
        private readonly SWPSolutionDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;
        public AdminService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IConfiguration config, SWPSolutionDBContext context, IEmailService emailService, IConfiguration configuration) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _context = context;
            _emailService = emailService;
            _configuration = configuration;
        }
        public async Task<string> AuthenticateAdmin(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return null;
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return null;
            }

            string adminId = await GetAdminIdByUsername(request.UserName);

            var getRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Name, request.UserName),
                new Claim("admin_id", adminId.ToString())
            };

            foreach (var role in getRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _config["JWT:Issuer"],
                _config["JWT:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> ExtractAdminIdFromTokenAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtPayloadBase64Url = token.Split('.')[1];
            var jwtPayloadBase64 = jwtPayloadBase64Url
                                    .Replace('-', '+')
                                    .Replace('_', '/')
                                    .PadRight(jwtPayloadBase64Url.Length + (4 - jwtPayloadBase64Url.Length % 4) % 4, '=');
            var jwtPayload = Encoding.UTF8.GetString(Convert.FromBase64String(jwtPayloadBase64));
            var jwtSecret = _config["JWT:SigningKey"];

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
            var adminId = principal.FindFirst("admin_id")?.Value;

            return adminId;
        }

        private async Task<string> GetAdminIdByUsername(string username)
        {
            var admin = _context.Staff.FirstOrDefault(m => m.Username == username);
            if (admin != null)
            {
                return admin.StaffId;
            }
            else
            {
                throw new SWPException("Error getting Admin ID");
            }
        }

        public async Task<bool> RegisterAdmin(RegisterRequest request)
        {
            var existingUsers = _userManager.Users.Where(u => u.UserName == request.UserName &&
                u.TemporaryPassword == request.Password &&
                u.EmailConfirmed == false)
    .ToList();
            if (existingUsers.Any())
            {
                foreach (var existingUser in existingUsers)
                {
                    var deleteResult = await _userManager.DeleteAsync(existingUser);
                    if (!deleteResult.Succeeded)
                    {
                        return false;
                    }
                }
            }

            var user = new AppUser()
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName,
                TemporaryPassword = request.Password,
                EmailConfirmed = false,
            };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return false; // If registration fails, return false
            }
            var otpSent = await SendOtp(request.Email);
            return true;
        }

        public async Task<bool> SendOtp(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;
            // Generate OTP
            var otp = new Random().Next(100000, 999999).ToString();
            // Save OTP and expiration to database
            user.EmailVerificationCode = otp;
            user.EmailVerificationExpiry = DateTime.Now.AddMinutes(10); // OTP expires in 10 minutes
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return false;
            }
            try
            {
                // Send OTP via email
                var message = new MessageVM(new string[] { user.Email }, "Confirm your email", $"<p>Your OTP is: {otp}</p>");
                _emailService.SendEmail(message);
                return true;
            }
            catch (Exception)
            {
                // If email sending fails, remove the user
                await _userManager.DeleteAsync(user);
                return false;
            }
        }

        public async Task<bool> ConfirmEmail(string otp)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.EmailVerificationCode == otp && u.EmailVerificationExpiry > DateTime.Now);
            if (user == null) return false;
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                user.EmailConfirmed = true;
                user.EmailVerificationCode = null;
                user.EmailVerificationExpiry = null;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    await transaction.RollbackAsync();
                    return false;
                }

                if (!await _roleManager.RoleExistsAsync("staffadmin"))
                {
                    var adminRole = new AppRole {Id = Guid.NewGuid(), Name = "staffadmin", Description = "Administrator role with full permissions" };
                    await _roleManager.CreateAsync(adminRole);
                }
                // Assign the admin role to the user
                await _userManager.AddToRoleAsync(user, "staffadmin");

                var adminId = GenerateAdminId();
                var admin = new Staff()
                {
                            StaffId = adminId,
                            FullName = $"{user.FirstName} {user.LastName}",
                            Username = user.UserName,
                            Password = user.TemporaryPassword,
                            Email = user.Email,
                            Phone = user.PhoneNumber,
                            Role = "staffadmin"
                };

                _context.Staff.Add(admin);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private string GenerateAdminId()
        {
            // Generate categories_ID based on current month, year, and auto-increment
            string month = DateTime.Now.ToString("MM");
            string year = DateTime.Now.ToString("yy");

            int autoIncrement = GetNextAutoIncrement(month, year);

            string formattedAutoIncrement = autoIncrement.ToString().PadLeft(3, '0');

            return $"SA{month}{year}{formattedAutoIncrement}";
        }

        private int GetNextAutoIncrement(string month, string year)
        {
            // Generate the pattern for categories_ID to match in SQL query
            string pattern = $"SA{month}{year}";

            // Retrieve the maximum auto-increment value from existing categories for the given month and year
            var maxAutoIncrement = _context.Staff
                .Where(c => c.StaffId.StartsWith(pattern))
                .Select(c => c.StaffId.Substring(6, 3)) // Select substring of auto-increment part
                .AsEnumerable() // Switch to client evaluation from this point
                .Select(s => int.Parse(s)) // Parse string to int
                .DefaultIfEmpty(0)
                .Max();

            return maxAutoIncrement + 1;
        }

        public async Task<StaffInfoVM> GetAdminById(string adminId)
        {
            var admin = _context.Staff.Find(adminId);
            if (admin == null) return null;

            return new StaffInfoVM
            {
                Id = adminId,
                Role = admin.Role,
                UserName = admin.Username,
                Password = admin.Password,
                Email = admin.Email,
                FullName = admin.FullName,
                PhoneNumber = admin.Phone
            };
        }

        public async Task<bool> UpdateAdmin(string id, UpdateAdminRequest request)
        {
            // Find the user by id
            var admin = _context.Staff.Find(id);
            if (admin == null)
            {
                return false;
            }

            // Update admin
            if (!string.IsNullOrEmpty(request.Password))
            {
                admin.Password = request.Password;
            }
            if (!string.IsNullOrEmpty(request.Fullname))
            {
                admin.FullName = request.Fullname;
            }
            if (!string.IsNullOrEmpty(request.Email))
            {
                admin.Email = request.Email;
            }
            if (!string.IsNullOrEmpty(request.Phone))
            {
                admin.Phone = request.Phone;
            }
            _context.Staff.Update(admin);

            // Update AppUser
            var user = await _userManager.FindByNameAsync(admin.Username);
            if (user != null && !string.IsNullOrEmpty(request.Password))
            {
                if (!string.IsNullOrEmpty(request.Password))
                {
                    // Reset the user's password using the provided password

                    var result = await _userManager.RemovePasswordAsync(user);
                    if (result.Succeeded)
                    {
                        result = await _userManager.AddPasswordAsync(user, request.Password);
                        if (!result.Succeeded)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    user.TemporaryPassword = request.Password;
                }
                var userUpdateResult = await _userManager.UpdateAsync(user);
                if (!userUpdateResult.Succeeded)
                {
                    return false;
                }
            }

            // Save all changes in one transaction
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ResetAdminPassword(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
                return false;

            var otpSent = await SendOtp(Email);

            return true;
        }

        public async Task<bool> ConfirmAdmin(string otp, UpdateStaffRequest request)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.EmailVerificationCode == otp && u.EmailVerificationExpiry > DateTime.Now);
            if (user == null) return false;
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                user.EmailConfirmed = true;
                user.EmailVerificationCode = null;
                user.EmailVerificationExpiry = null;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    await transaction.RollbackAsync();
                    return false;
                }

                var staff = _context.Staff.FirstOrDefault(s => s.Email == user.Email);
                if (staff == null)
                {
                    return false;
                }

                // Update staff password 
                if (!string.IsNullOrEmpty(request.Password))
                {
                    staff.Password = request.Password;
                }
                _context.Staff.Update(staff);

                //Update AppUser
                if (!string.IsNullOrEmpty(request.Password))
                {
                    var results = await _userManager.RemovePasswordAsync(user);
                    if (results.Succeeded)
                    {
                        result = await _userManager.AddPasswordAsync(user, request.Password);
                        if (!results.Succeeded)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    user.TemporaryPassword = request.Password;
                }
                var userUpdateResult = await _userManager.UpdateAsync(user);
                if (!userUpdateResult.Succeeded)
                {
                    return false;
                }
                //Save all changes in one transaction
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteAdmin(string adminId)
        {
            var admin = _context.Staff.Find(adminId);
            if (admin == null) return false;

            _context.Staff.Remove(admin);
            await _context.SaveChangesAsync();

            return true;
        }

        public ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            var validationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidAudience = _configuration["JWT:Issuer"],
                ValidIssuer = _configuration["JWT:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]))
            };

            return new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out SecurityToken validatedToken);
        }

        public async Task<bool> CreateBlogAsync(string staffId, BlogCreateRequest request)
        {
            var staff = await _context.Staff.FindAsync(staffId);
            if (staff == null) return false;

            string generatedId = GenerateBlogId();

            var blog = new Blog
            {
                BlogId = generatedId,
                Title = request.Title,
                Content = request.Content,
                Categories = request.Categories,
                DataCreate = DateTime.Now,
                StaffId = staffId
            };

            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return true;
        }

        private string GenerateBlogId()
        {
            // Generate categories_ID based on current month, year, and auto-increment
            string month = DateTime.Now.ToString("MM");
            string year = DateTime.Now.ToString("yy");

            int autoIncrement = GetBlogNextAutoIncrement(month, year);

            string formattedAutoIncrement = autoIncrement.ToString().PadLeft(3, '0');

            return $"BL{month}{year}{formattedAutoIncrement}";
        }

        private int GetBlogNextAutoIncrement(string month, string year)
        {
            string pattern = $"BL{month}{year}";

            var maxAutoIncrement = _context.Blogs
                .Where(c => c.BlogId.StartsWith(pattern))
                .Select(c => c.BlogId.Substring(6, 3))
                .AsEnumerable()
                .Select(s => int.Parse(s))
                .DefaultIfEmpty(0)
                .Max();

            return maxAutoIncrement + 1;
        }

        public async Task<List<Blog>> GetAllBlogsAsync()
        {
            var blogs = await _context.Blogs.ToListAsync();

            var blogDetails = blogs.Select(blog => new Blog
            {
                BlogId = blog.BlogId,
                Title = blog.Title,
                Content = blog.Content,
                Categories = blog.Categories,
                DataCreate = blog.DataCreate,
                StaffId = blog.StaffId
            }).OrderByDescending(m => m.BlogId).ToList();

            return blogDetails;
        }

        public async Task<BlogDetailVM> GetBlogByIdAsync(string id)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(a => a.BlogId == id || a.StaffId == id);
            if (blog == null) return null;

            return new BlogDetailVM
            {
                Id = blog.BlogId,
                Title = blog.Title,
                Content = blog.Content,
                Categories = blog.Categories,
                DateCreate = blog.DataCreate,
            };
        }

        public async Task<List<BlogDetailVM>> GetBlogByTitleAsync(string search)
        {
            var blogs = await _context.Blogs
                .Where(a => a.Title.Contains(search))
                .ToListAsync();

            if (blogs == null || blogs.Count == 0) return null;

            return blogs.Select(blog => new BlogDetailVM
            {
                Id = blog.BlogId,
                Title = blog.Title,
                Content = blog.Content,
                Categories = blog.Categories,
                DateCreate = blog.DataCreate,
            }).ToList();
        }

        public async Task<bool> UpdateBlogAsync(string staffId, UpdateBlogRequest request)
        {
            var blog = await _context.Blogs.FindAsync(staffId);
            if (blog == null) return false;

            if (!string.IsNullOrEmpty(request.Title))
                blog.Title = request.Title;

            if (!string.IsNullOrEmpty(request.Content))
                blog.Content = request.Content;

            if (!string.IsNullOrEmpty(request.Categories))
                blog.Categories = request.Categories;

            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteBlogAsync(string blogId)
        {
            var blog = await _context.Blogs.FindAsync(blogId);
            if (blog == null)
                return false;

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddOrder(string memberId, AddOrderRequest request)
        {
            var member = await _context.Members.FindAsync(memberId);
            if (member == null) return false;

            var order = new Order
            {
                OrderId = "",
                MemberId = memberId,
                PromotionId = request.PromotionId,
                ShippingAddress = request.ShippingAddress,
                TotalAmount = request.TotalAmount,
                //OrderStatus = request.OrderStatus,
                OrderDate = DateTime.Now,
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> UpdateOrder(string id, OrderUpdateRequest request)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;

             //   order.OrderStatus = request.orderStatus;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrder(string id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<OrderVM> GetOrderById(string id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return null;

            return new OrderVM
            {
                OrderId = order.OrderId,
                MemberId = order.MemberId,
                PromotionId = order.PromotionId,
                ShippingAddress = order.ShippingAddress,
                TotalAmount = (double)order.TotalAmount,
                OrderDate = (DateTime)order.OrderDate,
            };
        }

        public async Task<List<OrderVM>> GetAllOrder()
        {
            return await _context.Orders
                .Select(order => new OrderVM
                {
                    OrderId = order.OrderId,
                    MemberId = order.MemberId,
                    PromotionId = order.PromotionId,
                    ShippingAddress = order.ShippingAddress,
                    TotalAmount = (double)order.TotalAmount,
                    OrderDate = (DateTime)order.OrderDate,
                })
                .OrderByDescending(m => m.OrderId)
                .ToListAsync();
        }

        public async Task<Dictionary<DayOfWeek, int>> GetUserRegistrationsForCurrentWeek()
        {
            var currentDate = DateTime.Now;
            var currentCulture = CultureInfo.CurrentCulture;
            var firstDayOfWeek = currentCulture.DateTimeFormat.FirstDayOfWeek;

            // Calculate the start date of the current week
            var startDateOfWeek = currentDate.Date;
            while (startDateOfWeek.DayOfWeek != firstDayOfWeek)
            {
                startDateOfWeek = startDateOfWeek.AddDays(-1);
            }

            // Calculate the end date of the current week
            var endDateOfWeek = startDateOfWeek.AddDays(7);

            // Fetch the user registrations within the current week from the database
            var userRegistrations = await _context.Members
                .Where(u => u.RegistrationDate >= startDateOfWeek && u.RegistrationDate < endDateOfWeek)
                .ToListAsync();

            // Initialize a dictionary to store the number of registrations for each day of the week
            var registrationsByDay = new Dictionary<DayOfWeek, int>();

            // Populate the dictionary with all days of the week set to zero initially
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                registrationsByDay[day] = 0;
            }

            // Group the registrations by day of the week and calculate the totals in memory
            var groupedRegistrations = userRegistrations
                .GroupBy(u => u.RegistrationDate.DayOfWeek)
                .Select(g => new
                {
                    Day = g.Key,
                    RegistrationCount = g.Count()
                })
                .ToList();

            // Populate the dictionary with the results
            foreach (var registrationGroup in groupedRegistrations)
            {
                registrationsByDay[registrationGroup.Day] = registrationGroup.RegistrationCount;
            }

            return registrationsByDay;
        }

        public async Task<PageResult<BlogDetailVM>> GetBlogsPaging(GetUserPagingRequest request)
        {
            var query = _context.Blogs.AsQueryable();

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.StaffId.Contains(request.Keyword));
            }

            int totalRow = query.Count();

            var data = query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(blog => new BlogDetailVM()
                {
                    Id = blog.BlogId,
                    Title = blog.Title,
                    Content = blog.Content,
                    Categories = blog.Categories,
                    DateCreate = blog.DataCreate,
                    StaffId = blog.StaffId,
                }).OrderByDescending(m => m.Id).ToList();

            var pageResult = new PageResult<BlogDetailVM>
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data,
            };
            return pageResult;
        }

        public async Task<ApiResult<BlogDetailVM>> GetBlogIdPaging(string id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return new ApiErrorResult<BlogDetailVM>("Blog not exist");
            }

            var blogVm = new BlogDetailVM()
            {
                Id = blog.BlogId,
                Title = blog.Title,
                Content = blog.Content,
                Categories = blog.Categories,
                DateCreate = blog.DataCreate,
                StaffId = blog.StaffId,
            };
            return new ApiSuccessResult<BlogDetailVM>(blogVm);
        }

        //public async Task<bool> AddOrderTracking(string orderId, string staffId ,AddOrderTrackingRequest request)
        //{
        //    var order = await _context.Orders.FindAsync(orderId);
        //    if (order == null) return false;
        //    var staff = await _context.Staff.FindAsync(staffId);
        //    if (staff == null) return false;

        //    var tracking = new Order
        //    {
        //        OrderId = "",
        //        MemberId = memberId,
        //        PromotionId = request.PromotionId,
        //        ShippingAddress = request.ShippingAddress,
        //        TotalAmount = request.TotalAmount,
        //        //OrderStatus = request.OrderStatus,
        //        OrderDate = DateTime.Now,
        //    };

        //    _context..Add(order);
        //    await _context.SaveChangesAsync();

        //    return true;
        //}
        //public async Task<bool> UpdateOrder(string id, OrderUpdateRequest request)
        //{
        //    var order = await _context.Orders.FindAsync(id);
        //    if (order == null) return false;

        //    //   order.OrderStatus = request.orderStatus;

        //    _context.Orders.Update(order);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        //public async Task<bool> DeleteOrder(string id)
        //{
        //    var order = await _context.Orders.FindAsync(id);
        //    if (order == null) return false;

        //    _context.Orders.Remove(order);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        //public async Task<OrderVM> GetOrderById(string id)
        //{
        //    var order = await _context.Orders.FindAsync(id);
        //    if (order == null) return null;

        //    return new OrderVM
        //    {
        //        OrderId = order.OrderId,
        //        MemberId = order.MemberId,
        //        PromotionId = order.PromotionId,
        //        ShippingAddress = order.ShippingAddress,
        //        TotalAmount = (double)order.TotalAmount,
        //        OrderDate = (DateTime)order.OrderDate,
        //    };
        //}

        //public async Task<List<OrderVM>> GetAllOrder()
        //{
        //    return await _context.Orders
        //        .Select(order => new OrderVM
        //        {
        //            OrderId = order.OrderId,
        //            MemberId = order.MemberId,
        //            PromotionId = order.PromotionId,
        //            ShippingAddress = order.ShippingAddress,
        //            TotalAmount = (double)order.TotalAmount,
        //            OrderDate = (DateTime)order.OrderDate,
        //        })
        //        .OrderByDescending(m => m.OrderId)
        //        .ToListAsync();
        //}
    }
}
