using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Access.Sql.Configurations
{
    public class InvoiceDetailConfiguration : BaseEntityTypeConfiguration<InvoiceDetail>
    {
        public override void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => new { x.Id, x.InvoiceId, x.ClientProcedureId });

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
}
