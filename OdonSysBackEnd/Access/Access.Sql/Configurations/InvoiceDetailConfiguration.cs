using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Access.Sql.Configurations;

internal sealed class InvoiceDetailConfiguration : BaseEntityTypeConfiguration<InvoiceDetail>
{
    public override void Configure(EntityTypeBuilder<InvoiceDetail> builder)
    {
        base.Configure(builder);

        builder
            .HasIndex(x => new { x.InvoiceId, x.ClientProcedureId })
            .IsUnique();

        builder
            .HasOne(x => x.Invoice)
            .WithMany(x => x.InvoiceDetails)
            .HasForeignKey(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.ClientProcedure)
            .WithOne(x => x.InvoiceDetail)
            .HasForeignKey<InvoiceDetail>(x => x.ClientProcedureId);
    }
}
