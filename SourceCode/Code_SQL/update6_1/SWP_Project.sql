USE [master]
GO
/****** Object:  Database [SWP_Project]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE DATABASE [SWP_Project]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SWP_Project', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\SWP_Project.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SWP_Project_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\SWP_Project_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [SWP_Project] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SWP_Project].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SWP_Project] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SWP_Project] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SWP_Project] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SWP_Project] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SWP_Project] SET ARITHABORT OFF 
GO
ALTER DATABASE [SWP_Project] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SWP_Project] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SWP_Project] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SWP_Project] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SWP_Project] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SWP_Project] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SWP_Project] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SWP_Project] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SWP_Project] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SWP_Project] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SWP_Project] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SWP_Project] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SWP_Project] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SWP_Project] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SWP_Project] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SWP_Project] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SWP_Project] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SWP_Project] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SWP_Project] SET  MULTI_USER 
GO
ALTER DATABASE [SWP_Project] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SWP_Project] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SWP_Project] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SWP_Project] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SWP_Project] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SWP_Project] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [SWP_Project] SET QUERY_STORE = ON
GO
ALTER DATABASE [SWP_Project] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SWP_Project]
GO
USE [SWP_Project]
GO
/****** Object:  Sequence [dbo].[address_id_seq]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE SEQUENCE [dbo].[address_id_seq] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE 1
 MAXVALUE 999
 CYCLE 
 CACHE 
GO
USE [SWP_Project]
GO
/****** Object:  Sequence [dbo].[blog_id_seq]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE SEQUENCE [dbo].[blog_id_seq] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE 1
 MAXVALUE 999
 CYCLE 
 CACHE 
GO
USE [SWP_Project]
GO
/****** Object:  Sequence [dbo].[categories_id_seq]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE SEQUENCE [dbo].[categories_id_seq] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE 1
 MAXVALUE 999
 CYCLE 
 CACHE 
GO
USE [SWP_Project]
GO
/****** Object:  Sequence [dbo].[member_id_seq]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE SEQUENCE [dbo].[member_id_seq] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE 1
 MAXVALUE 999
 CYCLE 
 CACHE 
GO
USE [SWP_Project]
GO
/****** Object:  Sequence [dbo].[order_id_seq]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE SEQUENCE [dbo].[order_id_seq] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE 1
 MAXVALUE 999
 CYCLE 
 CACHE 
GO
USE [SWP_Project]
GO
/****** Object:  Sequence [dbo].[payment_id_seq]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE SEQUENCE [dbo].[payment_id_seq] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE 1
 MAXVALUE 999
 CYCLE 
 CACHE 
GO
USE [SWP_Project]
GO
/****** Object:  Sequence [dbo].[preorder_id_seq]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE SEQUENCE [dbo].[preorder_id_seq] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE 1
 MAXVALUE 999
 CYCLE 
 CACHE 
GO
USE [SWP_Project]
GO
/****** Object:  Sequence [dbo].[product_id_seq]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE SEQUENCE [dbo].[product_id_seq] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE 1
 MAXVALUE 999
 CYCLE 
 CACHE 
GO
USE [SWP_Project]
GO
/****** Object:  Sequence [dbo].[promotion_id_seq]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE SEQUENCE [dbo].[promotion_id_seq] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE 1
 MAXVALUE 999
 CYCLE 
 CACHE 
GO
USE [SWP_Project]
GO
/****** Object:  Sequence [dbo].[review_id_seq]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE SEQUENCE [dbo].[review_id_seq] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE 1
 MAXVALUE 999
 CYCLE 
 CACHE 
GO
USE [SWP_Project]
GO
/****** Object:  Sequence [dbo].[staff_id_seq]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE SEQUENCE [dbo].[staff_id_seq] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE 1
 MAXVALUE 999
 CYCLE 
 CACHE 
GO
/****** Object:  UserDefinedFunction [dbo].[generate_staff_id]    Script Date: 6/2/2024 11:42:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   FUNCTION [dbo].[generate_staff_id](@role VARCHAR(20))
RETURNS VARCHAR(10)
AS
BEGIN
    DECLARE @month CHAR(2) = FORMAT(GETDATE(), 'MM'); -- Lấy tháng hiện tại
    DECLARE @year CHAR(2) = FORMAT(GETDATE(), 'yy');  -- Lấy năm hiện tại
    DECLARE @auto_increment INT;

    SELECT @auto_increment = MAX(CAST(SUBSTRING(staff_ID, 7, 3) AS INT))
    FROM staff
    WHERE SUBSTRING(staff_ID, 3, 2) = @month
    AND SUBSTRING(staff_ID, 5, 2) = @year;

    IF @auto_increment IS NULL
        SET @auto_increment = 0;

    SET @auto_increment = @auto_increment + 1;

    DECLARE @formatted_auto_increment VARCHAR(3) = RIGHT('000' + CAST(@auto_increment AS VARCHAR(3)), 3);

    DECLARE @generated_staff_id VARCHAR(10) = CONCAT(CASE WHEN @role = 'SM' THEN 'SM' ELSE 'SA' END, @month, @year, @formatted_auto_increment);

    RETURN @generated_staff_id;
END;
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/2/2024 11:42:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 6/2/2024 11:42:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[address_ID] [varchar](10) NOT NULL,
	[member_ID] [varchar](10) NULL,
	[HouseNumber] [varchar](20) NULL,
	[Street] [nvarchar](50) NULL,
	[district] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[Region] [nvarchar](50) NULL,
 CONSTRAINT [PK__Address__CAA543F0AA445DBA] PRIMARY KEY CLUSTERED 
(
	[address_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppRoleClaims]    Script Date: 6/2/2024 11:42:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AppRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppRoles]    Script Date: 6/2/2024 11:42:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppRoles](
	[Id] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[NormalizedName] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AppRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppUserClaims]    Script Date: 6/2/2024 11:42:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AppUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppUserLogins]    Script Date: 6/2/2024 11:42:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUserLogins](
	[UserId] [uniqueidentifier] NOT NULL,
	[LoginProvider] [nvarchar](max) NOT NULL,
	[ProviderKey] [nvarchar](max) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
 CONSTRAINT [PK_AppUserLogins] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppUserRoles]    Script Date: 6/2/2024 11:42:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUserRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_AppUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppUsers]    Script Date: 6/2/2024 11:42:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUsers](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](200) NOT NULL,
	[LastName] [nvarchar](200) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[NormalizedUserName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[NormalizedEmail] [nvarchar](max) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[EmailVerificationCode] [nvarchar](max) NULL,
	[EmailVerificationExpiry] [datetime2](7) NULL,
 CONSTRAINT [PK_AppUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppUserTokens]    Script Date: 6/2/2024 11:42:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUserTokens](
	[UserId] [uniqueidentifier] NOT NULL,
	[LoginProvider] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AppUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blog]    Script Date: 6/2/2024 11:42:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog](
	[blog_ID] [varchar](10) NOT NULL,
	[title] [nvarchar](255) NULL,
	[content] [nvarchar](255) NULL,
	[categories] [nvarchar](255) NULL,
	[dataCreate] [date] NULL,
	[staff_ID] [varchar](10) NULL,
 CONSTRAINT [PK__Blog__298A9610ECF917C0] PRIMARY KEY CLUSTERED 
(
	[blog_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 6/2/2024 11:42:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[categories_ID] [varchar](10) NOT NULL,
	[brandName] [nvarchar](50) NULL,
	[AgeRange] [nvarchar](50) NULL,
	[SubCategories] [nvarchar](50) NULL,
	[packageType] [nvarchar](50) NULL,
	[source] [nvarchar](50) NULL,
 CONSTRAINT [PK__Categori__92BFEBD24C3D480D] PRIMARY KEY CLUSTERED 
(
	[categories_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 6/2/2024 11:42:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[member_ID] [varchar](10) NOT NULL,
	[FirstName] [nvarchar](10) NULL,
	[LastName] [nvarchar](10) NULL,
	[Email] [varchar](255) NULL,
	[PhoneNumber] [varchar](20) NULL,
	[LoyaltyPoints] [decimal](10, 2) NULL,
	[RegistrationDate] [date] NULL,
	[UserName] [varchar](50) NULL,
	[PassWord] [varchar](50) NULL,
 CONSTRAINT [PK__Member__B29A816CC54BDB96] PRIMARY KEY CLUSTERED 
(
	[member_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 6/2/2024 11:42:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[order_ID] [varchar](10) NOT NULL,
	[member_id] [varchar](10) NULL,
	[Promotion_ID] [varchar](10) NULL,
	[ShippingAddress] [nvarchar](50) NULL,
	[TotalAmount] [float] NULL,
	[orderStatus] [bit] NULL,
	[orderDate] [date] NULL,
 CONSTRAINT [PK__Order__464665E13F0051AC] PRIMARY KEY CLUSTERED 
(
	[order_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_detail]    Script Date: 6/2/2024 11:42:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_detail](
	[orderdetail_ID] [varchar](10) NOT NULL,
	[product_ID] [varchar](10) NULL,
	[order_ID] [varchar](10) NULL,
	[quantity] [int] NULL,
 CONSTRAINT [PK__Order_de__59AD78598BE8175A] PRIMARY KEY CLUSTERED 
(
	[orderdetail_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 6/2/2024 11:42:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[payment_ID] [varchar](10) NOT NULL,
	[order_ID] [varchar](10) NULL,
	[amount] [decimal](10, 2) NULL,
	[discountValue] [float] NULL,
	[paymentStatus] [bit] NULL,
	[PaymentMethod] [varchar](50) NULL,
	[PaymentDate] [date] NULL,
 CONSTRAINT [PK__Payment__ED10C4420D3DCCF4] PRIMARY KEY CLUSTERED 
(
	[payment_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PreOrder]    Script Date: 6/2/2024 11:42:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PreOrder](
	[preorder_ID] [varchar](10) NOT NULL,
	[product_ID] [varchar](10) NULL,
	[member_ID] [varchar](10) NULL,
	[Quantity] [int] NULL,
	[preorderDate] [date] NULL,
	[price] [float] NULL,
 CONSTRAINT [PK__PreOrder__C55D7EA295C14F89] PRIMARY KEY CLUSTERED 
(
	[preorder_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 6/2/2024 11:42:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[product_ID] [varchar](10) NOT NULL,
	[categories_ID] [varchar](10) NULL,
	[ProductName] [nvarchar](50) NULL,
	[Quantity] [int] NULL,
	[Price] [float] NULL,
	[Description] [nvarchar](255) NULL,
	[statusDescription] [varchar](50) NULL,
	[image] [text] NULL,
 CONSTRAINT [PK__Product__470175FDED17C147] PRIMARY KEY CLUSTERED 
(
	[product_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductImages]    Script Date: 6/2/2024 11:42:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductImages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [varchar](10) NOT NULL,
	[ImagePath] [nvarchar](200) NOT NULL,
	[Caption] [nvarchar](200) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[SortOrder] [int] NOT NULL,
	[FileSize] [bigint] NOT NULL,
 CONSTRAINT [PK_ProductImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Promotion]    Script Date: 6/2/2024 11:42:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promotion](
	[promotion_ID] [varchar](10) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[DiscountType] [varchar](50) NULL,
	[DiscountValue] [int] NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
 CONSTRAINT [PK__Promotio__2C45E8433ED651C3] PRIMARY KEY CLUSTERED 
(
	[promotion_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Review]    Script Date: 6/2/2024 11:42:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Review](
	[review_ID] [varchar](10) NOT NULL,
	[product_ID] [varchar](10) NULL,
	[member_ID] [varchar](10) NULL,
	[dataReview] [date] NULL,
	[Grade] [int] NULL,
	[comment] [nvarchar](255) NULL,
 CONSTRAINT [PK__Review__608B39D8185D9A34] PRIMARY KEY CLUSTERED 
(
	[review_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[staff]    Script Date: 6/2/2024 11:42:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[staff](
	[staff_ID] [varchar](10) NOT NULL,
	[role] [varchar](20) NOT NULL,
	[username] [varchar](50) NULL,
	[password] [varchar](50) NULL,
	[fullName] [nvarchar](50) NULL,
	[Email] [varchar](255) NULL,
	[phone] [text] NULL,
 CONSTRAINT [PK__staff__196CD194F520350A] PRIMARY KEY CLUSTERED 
(
	[staff_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Address_member_ID]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_Address_member_ID] ON [dbo].[Address]
(
	[member_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Blog_staff_ID]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_Blog_staff_ID] ON [dbo].[Blog]
(
	[staff_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Order_member_id]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_Order_member_id] ON [dbo].[Order]
(
	[member_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Order_Promotion_ID]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_Order_Promotion_ID] ON [dbo].[Order]
(
	[Promotion_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Order_detail_order_ID]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_Order_detail_order_ID] ON [dbo].[Order_detail]
(
	[order_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Order_detail_product_ID]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_Order_detail_product_ID] ON [dbo].[Order_detail]
(
	[product_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Payment_order_ID]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_Payment_order_ID] ON [dbo].[Payment]
(
	[order_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_PreOrder_member_ID]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_PreOrder_member_ID] ON [dbo].[PreOrder]
(
	[member_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_PreOrder_product_ID]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_PreOrder_product_ID] ON [dbo].[PreOrder]
(
	[product_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Product_categories_ID]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_Product_categories_ID] ON [dbo].[Product]
(
	[categories_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ProductImages_ProductId]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_ProductImages_ProductId] ON [dbo].[ProductImages]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Review_member_ID]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_Review_member_ID] ON [dbo].[Review]
(
	[member_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Review_product_ID]    Script Date: 6/2/2024 11:42:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_Review_product_ID] ON [dbo].[Review]
(
	[product_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [fk_Address] FOREIGN KEY([member_ID])
REFERENCES [dbo].[Member] ([member_ID])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [fk_Address]
GO
ALTER TABLE [dbo].[Blog]  WITH CHECK ADD  CONSTRAINT [fk_blog_staff] FOREIGN KEY([staff_ID])
REFERENCES [dbo].[staff] ([staff_ID])
GO
ALTER TABLE [dbo].[Blog] CHECK CONSTRAINT [fk_blog_staff]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [fk_order_member] FOREIGN KEY([member_id])
REFERENCES [dbo].[Member] ([member_ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [fk_order_member]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [fk_order_promotion] FOREIGN KEY([Promotion_ID])
REFERENCES [dbo].[Promotion] ([promotion_ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [fk_order_promotion]
GO
ALTER TABLE [dbo].[Order_detail]  WITH CHECK ADD  CONSTRAINT [fk_orderdetail_order] FOREIGN KEY([order_ID])
REFERENCES [dbo].[Order] ([order_ID])
GO
ALTER TABLE [dbo].[Order_detail] CHECK CONSTRAINT [fk_orderdetail_order]
GO
ALTER TABLE [dbo].[Order_detail]  WITH CHECK ADD  CONSTRAINT [fk_orderdetail_product] FOREIGN KEY([product_ID])
REFERENCES [dbo].[Product] ([product_ID])
GO
ALTER TABLE [dbo].[Order_detail] CHECK CONSTRAINT [fk_orderdetail_product]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [fk_Payment_order] FOREIGN KEY([order_ID])
REFERENCES [dbo].[Order] ([order_ID])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [fk_Payment_order]
GO
ALTER TABLE [dbo].[PreOrder]  WITH CHECK ADD  CONSTRAINT [fk_PreOrder_member] FOREIGN KEY([member_ID])
REFERENCES [dbo].[Member] ([member_ID])
GO
ALTER TABLE [dbo].[PreOrder] CHECK CONSTRAINT [fk_PreOrder_member]
GO
ALTER TABLE [dbo].[PreOrder]  WITH CHECK ADD  CONSTRAINT [fk_PreOrder_Product] FOREIGN KEY([product_ID])
REFERENCES [dbo].[Product] ([product_ID])
GO
ALTER TABLE [dbo].[PreOrder] CHECK CONSTRAINT [fk_PreOrder_Product]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [fk_Product_categories] FOREIGN KEY([categories_ID])
REFERENCES [dbo].[Categories] ([categories_ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [fk_Product_categories]
GO
ALTER TABLE [dbo].[ProductImages]  WITH CHECK ADD  CONSTRAINT [FK_ProductImages_Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([product_ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductImages] CHECK CONSTRAINT [FK_ProductImages_Product_ProductId]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [fk_review_member] FOREIGN KEY([member_ID])
REFERENCES [dbo].[Member] ([member_ID])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [fk_review_member]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [fk_review_Product] FOREIGN KEY([product_ID])
REFERENCES [dbo].[Product] ([product_ID])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [fk_review_Product]
GO
USE [master]
GO
ALTER DATABASE [SWP_Project] SET  READ_WRITE 
GO
