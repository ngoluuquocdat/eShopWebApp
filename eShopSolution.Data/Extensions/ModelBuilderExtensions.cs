using eShopSolution.Data.Entities;
using eShopSolution.Data.Enums;
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
                new Language() { Id="vi-VN", Name="Tiếng Việt", IsDefault=true},
                new Language() { Id = "en-US", Name = "English", IsDefault = false });           

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
                    LanguageId = "vi-VN",
                    SeoAlias = "ao-nam",
                    SeoDescription = "Sản phẩm áo thời trang nam",
                    SeoTitle = "Sản phẩm áo thời trang nam"
                },
                new CategoryTranslation()
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Men Shirt",
                    LanguageId = "en-US",
                    SeoAlias = "men-shirt",
                    SeoDescription = "The shirt products for men",
                    SeoTitle = "The shirt products for men"
                },
                new CategoryTranslation()
                {
                    Id = 3,
                    CategoryId = 2,
                    Name = "Áo nữ",
                    LanguageId = "vi-VN",
                    SeoAlias = "ao-nu",
                    SeoDescription = "Sản phẩm áo thời trang nữ",
                    SeoTitle = "Sản phẩm áo thời trang nữ"
                },
                new CategoryTranslation()
                {
                    Id = 4,
                    CategoryId = 2,
                    Name = "Women Shirt",
                    LanguageId = "en-US",
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
                    LanguageId = "vi-VN",
                    SeoAlias = "ao-gym-nam",
                    SeoDescription = "Áo nam tập gym",
                    SeoTitle = "Áo nam tập gym",
                    Details = "chi tiết sản phẩm",
                    Description = ""
                },
                new ProductTranslation()
                {
                    Id = 2,
                    ProductId = 1,
                    Name = "Men Gym Sleeveless Shirt",
                    LanguageId = "en-US",
                    SeoAlias = "men-gym-shirt",
                    SeoDescription = "Sleeveless shirt for men gymmer",
                    SeoTitle = "Deep cut sleeveless shirt, oversized from",
                    Details = "product details",
                    Description = ""
                }
            );

            // data seeding cho ProductInCategories
            modelBuilder.Entity<ProductInCategory>().HasData(
                new ProductInCategory() 
                { 
                    ProductId = 1,
                    CategoryId = 1 }
                );

        }
    }
}
