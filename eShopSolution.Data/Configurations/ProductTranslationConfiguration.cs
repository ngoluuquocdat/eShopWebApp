using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    public class ProductTranslationConfiguration : IEntityTypeConfiguration<ProductTranslation>
    {
        public void Configure(EntityTypeBuilder<ProductTranslation> builder)
        {
            builder.ToTable("ProductTranslations");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.SeoAlias).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Details).HasMaxLength(500);
            builder.Property(x => x.LanguageId).IsUnicode(false).IsRequired().HasMaxLength(5);

            // 1-n: product and productTranslation
            builder.HasOne(pt => pt.Product)
                .WithMany(p => p.ProductTranslations)
                .HasForeignKey(pt => pt.ProductId);

            // 1-n: language and productTranslation
            builder.HasOne(pt => pt.Language)
                .WithMany(l => l.ProductTranslations)
                .HasForeignKey(pt => pt.LanguageId);
        }
    }
}
