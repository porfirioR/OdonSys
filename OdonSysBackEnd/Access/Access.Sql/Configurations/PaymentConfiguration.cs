using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.Sql.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x => new { x.Id, x.HeaderBillId, x.UserId });

            builder
                .Property(d => d.DateCreated)
                .HasDefaultValueSql("GetDate()");

            builder
                .Property(d => d.DateModified)
                .HasDefaultValueSql("GetDate()");

            builder
                .HasOne(x => x.HeaderBill)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.HeaderBillId);

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.UserId);

        }
    }
}
