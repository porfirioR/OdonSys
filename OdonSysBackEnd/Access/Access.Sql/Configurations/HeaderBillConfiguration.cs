using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Utilities.Enums;
using Utilities.Extensions;

namespace Access.Sql.Configurations
{
    public class HeaderBillConfiguration : IEntityTypeConfiguration<HeaderBill>
    {
        public void Configure(EntityTypeBuilder<HeaderBill> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(d => d.DateCreated)
                .HasDefaultValueSql("GetDate()");

            builder
                .Property(d => d.DateModified)
                .HasDefaultValueSql("GetDate()");

            builder
                .Property(x => x.BillNumber)
                .IsRequired()
                .HasMaxLength(25);

            builder
                .Property(x => x.Timbrado)
                .HasMaxLength(25);

            builder
                .Property(x => x.Total)
                .HasDefaultValue(0);

            builder.Property(s => s.Status)
               .HasConversion(
                   s => s.GetDescription(),
                   s => (BillStatus)Enum.Parse(typeof(PermissionName), s));

            builder
                .HasOne(x => x.Client)
                .WithMany(x => x.HeaderBills)
                .HasForeignKey(x => x.ClientId);
        }
    }
}
