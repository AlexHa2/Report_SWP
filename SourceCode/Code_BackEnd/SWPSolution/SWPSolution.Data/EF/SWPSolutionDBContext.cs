﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SWPSolution.Data.Configuration;
using SWPSolution.Data.Extension;

namespace SWPSolution.Data.Entities;

public partial class SWPSolutionDBContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public SWPSolutionDBContext()
    {
    }

    public SWPSolutionDBContext(DbContextOptions<SWPSolutionDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PreOrder> PreOrders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.

        => optionsBuilder.UseSqlServer("Data Source=113.173.196.176;Initial Catalog=SWP_Project;User ID=sa;Password=Thomas1910@;Encrypt=True;Trust Server Certificate=True");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>()
            .Property(u => u.EmailVerificationCode)
            .IsRequired(false);

        modelBuilder.Entity<AppUser>()
            .Property(u => u.EmailVerificationExpiry)
            .IsRequired(false);

        modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
        modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new
        {
            x.UserId,
            x.RoleId
        });
        ;
        modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
        modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);



        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Address__CAA543F0AA445DBA");

            entity.ToTable("Address", tb => tb.HasTrigger("trg_generate_address_id"));

            entity.Property(e => e.AddressId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("address_ID");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.District)
                .HasMaxLength(50)
                .HasColumnName("district");
            entity.Property(e => e.HouseNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MemberId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("member_ID");
            entity.Property(e => e.Region).HasMaxLength(50);
            entity.Property(e => e.Street).HasMaxLength(50);

            entity.HasOne(d => d.Member).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("fk_Address");
        });

        modelBuilder.ApplyConfiguration(new AppUserConfig());
        modelBuilder.ApplyConfiguration(new AppRoleConfig());
        modelBuilder.ApplyConfiguration(new ProductImageConfig());

        

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__Blog__298A9610ECF917C0");

            entity.ToTable("Blog", tb => tb.HasTrigger("trg_generate_blog_ID"));

            entity.Property(e => e.BlogId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("blog_ID");
            entity.Property(e => e.Categories)
                .HasMaxLength(255)
                .HasColumnName("categories");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .HasColumnName("content");
            entity.Property(e => e.DateCreate)
                .HasColumnType("date")
                .HasColumnName("dateCreate");
            entity.Property(e => e.StaffId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("staff_ID");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.Staff).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("fk_blog_staff");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoriesId).HasName("PK__Categori__92BFEBD24C3D480D");

            entity.ToTable(tb => tb.HasTrigger("trg_generate_categories_id"));

            entity.Property(e => e.CategoriesId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("categories_ID");
            entity.Property(e => e.AgeRange).HasMaxLength(50);
            entity.Property(e => e.BrandName)
                .HasMaxLength(50)
                .HasColumnName("brandName");
            entity.Property(e => e.PackageType)
                .HasMaxLength(50)
                .HasColumnName("packageType");
            entity.Property(e => e.Source)
                .HasMaxLength(50)
                .HasColumnName("source");
            entity.Property(e => e.SubCategories).HasMaxLength(50);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Member__B29A816CC54BDB96");

            entity.ToTable("Member", tb => tb.HasTrigger("trg_generate_member_id"));

            entity.Property(e => e.MemberId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("member_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(10);
            entity.Property(e => e.LastName).HasMaxLength(10);
            entity.Property(e => e.LoyaltyPoints).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PassWord)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationDate).HasColumnType("date");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__464665E13F0051AC");

            entity.ToTable("Order", tb => tb.HasTrigger("trg_generate_order_id"));

            entity.Property(e => e.OrderId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("order_ID");
            entity.Property(e => e.MemberId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("member_id");
            entity.Property(e => e.OrderDate)
                .HasColumnType("date")
                .HasColumnName("orderDate");
            entity.Property(e => e.OrderStatus).HasColumnName("orderStatus");
            entity.Property(e => e.PromotionId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Promotion_ID");
            entity.Property(e => e.ShippingAddress).HasMaxLength(50);

            entity.HasOne(d => d.Member).WithMany(p => p.Orders)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("fk_order_member");

            entity.HasOne(d => d.Promotion).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PromotionId)
                .HasConstraintName("fk_order_promotion");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderdetailId).HasName("PK__Order_de__59AD78598BE8175A");

            entity.ToTable("Order_detail");

            entity.Property(e => e.OrderdetailId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("orderdetail_ID");
            entity.Property(e => e.OrderId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("order_ID");
            entity.Property(e => e.ProductId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("product_ID");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("fk_orderdetail_order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fk_orderdetail_product");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__ED10C4420D3DCCF4");

            entity.ToTable("Payment", tb => tb.HasTrigger("trg_generate_payment_ID"));

            entity.Property(e => e.PaymentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("payment_ID");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.DiscountValue).HasColumnName("discountValue");
            entity.Property(e => e.OrderId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("order_ID");
            entity.Property(e => e.PaymentDate).HasColumnType("date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaymentStatus).HasColumnName("paymentStatus");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("fk_Payment_order");
        });

        modelBuilder.Entity<PreOrder>(entity =>
        {
            entity.HasKey(e => e.PreorderId).HasName("PK__PreOrder__C55D7EA295C14F89");

            entity.ToTable("PreOrder", tb => tb.HasTrigger("trg_generate_preorder_ID"));

            entity.Property(e => e.PreorderId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("preorder_ID");
            entity.Property(e => e.MemberId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("member_ID");
            entity.Property(e => e.PreorderDate)
                .HasColumnType("date")
                .HasColumnName("preorderDate");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProductId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("product_ID");

            entity.HasOne(d => d.Member).WithMany(p => p.PreOrders)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("fk_PreOrder_member");

            entity.HasOne(d => d.Product).WithMany(p => p.PreOrders)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fk_PreOrder_Product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__470175FDED17C147");

            entity.ToTable("Product", tb => tb.HasTrigger("trg_generate_product_id"));

            entity.Property(e => e.ProductId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("product_ID")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.CategoriesId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("categories_ID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Image)
                .HasColumnType("text")
                .HasColumnName("image");
            entity.Property(e => e.ProductName).HasMaxLength(50);
            entity.Property(e => e.StatusDescription)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("statusDescription");

            entity.HasOne(d => d.Categories).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoriesId)
                .HasConstraintName("fk_Product_categories");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("PK__Promotio__2C45E8433ED651C3");

            entity.ToTable("Promotion", tb => tb.HasTrigger("trg_generate_promotion_id"));

            entity.Property(e => e.PromotionId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("promotion_ID");
            entity.Property(e => e.DiscountType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.StartDate).HasColumnType("date");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Review__608B39D8185D9A34");

            entity.ToTable("Review", tb => tb.HasTrigger("trg_generate_review_ID"));

            entity.Property(e => e.ReviewId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("review_ID");
            entity.Property(e => e.Comment)
                .HasMaxLength(255)
                .HasColumnName("comment");
            entity.Property(e => e.DataReview)
                .HasColumnType("date")
                .HasColumnName("dataReview");
            entity.Property(e => e.MemberId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("member_ID");
            entity.Property(e => e.ProductId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("product_ID");

            entity.HasOne(d => d.Member).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("fk_review_member");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fk_review_Product");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__staff__196CD194F520350A");

            entity.ToTable("staff", tb => tb.HasTrigger("trg_generate_staff_id"));

            entity.Property(e => e.StaffId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("staff_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .HasColumnName("fullName");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasColumnType("text")
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });
        modelBuilder.HasSequence("address_id_seq")
            .HasMin(1L)
            .HasMax(999L)
            .IsCyclic();
        modelBuilder.HasSequence("blog_id_seq")
            .HasMin(1L)
            .HasMax(999L)
            .IsCyclic();
        modelBuilder.HasSequence("categories_id_seq")
            .HasMin(1L)
            .HasMax(999L)
            .IsCyclic();
        modelBuilder.HasSequence("member_id_seq")
            .HasMin(1L)
            .HasMax(999L)
            .IsCyclic();
        modelBuilder.HasSequence("order_id_seq")
            .HasMin(1L)
            .HasMax(999L)
            .IsCyclic();
        modelBuilder.HasSequence("payment_id_seq")
            .HasMin(1L)
            .HasMax(999L)
            .IsCyclic();
        modelBuilder.HasSequence("preorder_id_seq")
            .HasMin(1L)
            .HasMax(999L)
            .IsCyclic();
        modelBuilder.HasSequence("product_id_seq")
            .HasMin(1L)
            .HasMax(999L)
            .IsCyclic();
        modelBuilder.HasSequence("promotion_id_seq")
            .HasMin(1L)
            .HasMax(999L)
            .IsCyclic();
        modelBuilder.HasSequence("review_id_seq")
            .HasMin(1L)
            .HasMax(999L)
            .IsCyclic();
        modelBuilder.HasSequence("staff_id_seq")
            .HasMin(1L)
            .HasMax(999L)
            .IsCyclic();

        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Seed();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}