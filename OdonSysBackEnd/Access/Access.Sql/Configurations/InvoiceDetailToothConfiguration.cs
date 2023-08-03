using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Access.Sql.Configurations
{
    public class InvoiceDetailToothConfiguration : BaseEntityTypeConfiguration<InvoiceDetailTooth>
    {
        public override void Configure(EntityTypeBuilder<InvoiceDetailTooth> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => new { x.Id, x.InvoiceDetailId, x.ToothId });

            builder
                .HasOne(x => x.InvoiceDetail)
                .WithMany(x => x.InvoiceDetailsTeeth)
                .HasForeignKey(x => x.InvoiceDetailId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Tooth)
                .WithMany(x => x.InvoiceDetailsTeeth)
                .HasForeignKey(x => x.ToothId);
        }
    }
}
