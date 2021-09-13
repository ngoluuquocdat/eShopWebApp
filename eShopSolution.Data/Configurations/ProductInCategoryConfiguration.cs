using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    public class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.ToTable("ProductInCategories");
            builder.HasKey(t => new { t.CategoryId, t.ProductId }); // có 2 keys

         // liên kết khóa ngoại đến 2 bảng product và category, 
         // trung gian cho many-to-many giữa product và category
            // 1 product với nhiều ProductInCategories
            builder.HasOne(pc => pc.Product)            // có 1 ProductInCategory.Product
                .WithMany(p => p.ProductInCategories)   // với nhiều Product.ProductInCategories
                .HasForeignKey(pc => pc.ProductId);     // khóa ngoại là ProductId
            // => 1 ProductInCategory có 1 (HasOne) product, với nhiều (WithMany) ProductInCategory trong 1 product(p.ProductInCategories)

            // 1 category với nhiều ProductInCategories
            builder.HasOne(pc => pc.Category)
                .WithMany(c => c.ProductInCategories)
                .HasForeignKey(pc => pc.CategoryId);
        }
    }
}
