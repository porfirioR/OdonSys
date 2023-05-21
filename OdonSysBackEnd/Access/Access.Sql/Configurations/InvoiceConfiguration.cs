using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utilities.Enums;
using Utilities.Extensions;

namespace Access.Sql.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.DateCreated)
                .HasDefaultValueSql("GetDate()");

            builder
                .Property(x => x.DateModified)
                .HasDefaultValueSql("GetDate()");

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
