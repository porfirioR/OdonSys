using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Access.Sql.Configurations
{
    public class PaymentConfiguration : BaseEntityTypeConfiguration<Payment>
    {
        public override void Configure(EntityTypeBuilder<Payment> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => new { x.Id, x.InvoiceId, x.UserId });

            builder
                .HasOne(x => x.Invoice)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.InvoiceId);

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.UserId);
        }
    }
}
