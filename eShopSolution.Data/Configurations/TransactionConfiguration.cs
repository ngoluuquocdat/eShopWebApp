using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(x => x.Id);

            // config này là vì mysql không hỗ trợ default value cho empty Guid
            // nếu dùng Sql Server thì khỏi có dòng này:
            //builder.Property(x => x.UserId).HasDefaultValue(new Guid("00000000-0000-0000-0000-000000000000"));
            // 1 Guid chiếm 16 bytes, và 1 empty Guid sẽ là dãy số 0 như trên

            // 1-n: AppUser - Transactions
            builder.HasOne(tr => tr.AppUser)
                .WithMany(u => u.Transactions)
                .HasForeignKey(tr => tr.UserId);
        }
    }
}
