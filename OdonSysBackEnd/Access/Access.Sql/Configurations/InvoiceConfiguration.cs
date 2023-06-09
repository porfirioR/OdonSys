using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utilities.Enums;
using Utilities.Extensions;

namespace Access.Sql.Configurations
{
    public class InvoiceConfiguration : BaseEntityTypeConfiguration<Invoice>
    {
        public override void Configure(EntityTypeBuilder<Invoice> builder)
        {
            base.Configure(builder);

            builder
                .Property(x => x.InvoiceNumber)
                .IsRequired()
                .HasMaxLength(25);

            builder
                .Property(x => x.Timbrado)
                .HasMaxLength(25);

            builder
                .Property(x => x.Total)
                .HasDefaultValue(0);

            builder.Property(x => x.Status)
               .HasConversion(
                   x => x.GetDescription(),
                   x => (InvoiceStatus)Enum.Parse(typeof(InvoiceStatus), x));

            builder
                .HasOne(x => x.Client)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.ClientId);
        }
    }
}
