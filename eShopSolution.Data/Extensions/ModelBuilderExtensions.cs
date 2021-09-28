﻿using eShopSolution.Data.Entities;
using eShopSolution.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Extensions
{
    // viết các extension methods cho class ModelBuilder trong việc seeding data
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // data seeding cho AppConfigs
            modelBuilder.Entity<AppConfig>().HasData(
                new AppConfig { Key = "HomeTitle", Value = "This is home page of eShop" },
                new AppConfig { Key = "HomeKeyword", Value = "This is keyword of eShop" },
                new AppConfig { Key = "HomeDescription", Value = "This is description of eShop" }
            );
            //data seeding cho Languagues
            modelBuilder.Entity<Language>().HasData(
                new Language() { Id="vi", Name="Tiếng Việt", IsDefault=true},
                new Language() { Id ="en", Name = "English", IsDefault = false });           

            // data seeding cho Categories
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 1,
                    Status = Status.Active,
                    
                },
                new Category
                {
                    Id = 2,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 2,
                    Status = Status.Active,                  
                }
            );

            // data seeding cho CategoryTranslations
            modelBuilder.Entity<CategoryTranslation>().HasData(
                new CategoryTranslation()
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "Áo nam",
                    LanguageId = "vi",
                    SeoAlias = "ao-nam",
                    SeoDescription = "Sản phẩm áo thời trang nam",
                    SeoTitle = "Sản phẩm áo thời trang nam"
                },
                new CategoryTranslation()
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Men Shirt",
                    LanguageId = "en",
                    SeoAlias = "men-shirt",
                    SeoDescription = "The shirt products for men",
                    SeoTitle = "The shirt products for men"
                },
                new CategoryTranslation()
                {
                    Id = 3,
                    CategoryId = 2,
                    Name = "Áo nữ",
                    LanguageId = "vi",
                    SeoAlias = "ao-nu",
                    SeoDescription = "Sản phẩm áo thời trang nữ",
                    SeoTitle = "Sản phẩm áo thời trang nữ"
                },
                new CategoryTranslation()
                {
                    Id = 4,
                    CategoryId = 2,
                    Name = "Women Shirt",
                    LanguageId = "en",
                    SeoAlias = "women-shirt",
                    SeoDescription = "The shirt products for women",
                    SeoTitle = "The shirt products for women"
                }
                );

            // data seeding cho Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 100000,
                    Price = 200000,
                    Stock = 0,
                    ViewCount = 0,
                }
            );

            // data seeding cho ProductTranslations
            modelBuilder.Entity<ProductTranslation>().HasData(
                new ProductTranslation()
                {
                    Id = 1,
                    ProductId = 1,
                    Name = "Áo sát nách gym nam",
                    LanguageId = "vi",
                    SeoAlias = "ao-gym-nam",
                    SeoDescription = "Áo nam tập gym",
                    SeoTitle = "Áo nam tập gym",
                    Details = "chi tiết sản phẩm",
                    Description = "Áo sát nách khoét sâu, form rộng"
                },
                new ProductTranslation()
                {
                    Id = 2,
                    ProductId = 1,
                    Name = "Men Gym Sleeveless Shirt",
                    LanguageId = "en",
                    SeoAlias = "men-gym-shirt",
                    SeoDescription = "Sleeveless shirt for men gymmer",
                    SeoTitle = "Sleeveless shirt for men gymmer",
                    Details = "product details",
                    Description = "Deep cut sleeveless shirt, oversized formed"
                }
            );

            // data seeding cho ProductInCategories
            modelBuilder.Entity<ProductInCategory>().HasData(
                new ProductInCategory() 
                { 
                    ProductId = 1,
                    CategoryId = 1 }
                );

            // data seeding cho AppRoles: ở đây tạo role admin
            // can use any guid
            var ADMIN_ROLE_ID = new Guid("5810934D-2E2A-4E8F-ABE6-A4F1EA58FE37");
            var USER_ROLE_ID = new Guid("C4E9559F-505B-44C8-B1BA-1701739B6AE7");
            modelBuilder.Entity<AppRole>().HasData(
                new AppRole()
                {
                    Id = ADMIN_ROLE_ID,     
                    Name = "admin",
                    NormalizedName = "admin",
                    Description = "Administrator role"
                },
                new AppRole()
                {
                    Id = USER_ROLE_ID,
                    Name = "user",
                    NormalizedName = "user",
                    Description = "Normal User role"
                }
            );


            // data seeding cho AppUsers: ở đây tạo user admin
            // can use any guid
            var ADMIN_ID = new Guid("F9898841-3761-47F9-8ED3-EBA4A62B2FE5");
            

            var hasher = new PasswordHasher<AppUser>();      // tạo 1 hasher cho entity AppUser
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = ADMIN_ID,    
                UserName = "admin",     
                NormalizedUserName = "admin",
                Email = "ngoluuquocdat@gmail.com",
                NormalizedEmail = "ngoluuquocdat@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "123456"),   // password được hash từ plain text
                SecurityStamp = string.Empty,
                FirstName = "Quốc Đạt",
                LastName = "Ngô Lưu",
                Dob = new DateTime(2000,10,18)      // params: int year, int month, int day
            });

            // data seeding cho IdentityUserRole: gán user admin vào role admin
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_ID
            });

            modelBuilder.Entity<Slide>().HasData(
              new Slide() { Id = 1, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 1, Url = "#", Image = "/themes/images/carousel/1.png", Status = Status.Active },
              new Slide() { Id = 2, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 2, Url = "#", Image = "/themes/images/carousel/2.png", Status = Status.Active },
              new Slide() { Id = 3, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 3, Url = "#", Image = "/themes/images/carousel/3.png", Status = Status.Active },
              new Slide() { Id = 4, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 4, Url = "#", Image = "/themes/images/carousel/4.png", Status = Status.Active },
              new Slide() { Id = 5, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 5, Url = "#", Image = "/themes/images/carousel/5.png", Status = Status.Active },
              new Slide() { Id = 6, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 6, Url = "#", Image = "/themes/images/carousel/6.png", Status = Status.Active }
              );
        }
    }
}
