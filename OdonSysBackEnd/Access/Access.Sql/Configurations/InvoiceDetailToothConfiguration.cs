using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Access.Sql.Configurations;

internal sealed class InvoiceDetailToothConfiguration : BaseEntityTypeConfiguration<InvoiceDetailTooth>
{
    public override void Configure(EntityTypeBuilder<InvoiceDetailTooth> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(x => x.InvoiceDetail)
            .WithMany(x => x.InvoiceDetailsTeeth)
            .HasForeignKey(x => x.InvoiceDetailId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Tooth)
            .WithMany(x => x.InvoiceDetailsTeeth)
            .HasForeignKey(x => x.ToothId);
    }
}
