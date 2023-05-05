using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Access.Sql.Configurations
{
    public class BillDetailConfiguration : IEntityTypeConfiguration<BillDetail>
    {
        public void Configure(EntityTypeBuilder<BillDetail> builder)
        {
            builder.HasKey(x => new { x.Id, x.HeaderBillId, x.ClientProcedureId });

            builder
                .Property(d => d.DateCreated)
                .HasDefaultValueSql("GetDate()");

            builder
                .Property(d => d.DateModified)
                .HasDefaultValueSql("GetDate()");

            builder
                .HasOne(x => x.HeaderBill)
                .WithMany(x => x.BillDetails)
                .HasForeignKey(x => x.HeaderBillId);

            builder
                .HasOne(x => x.ClientProcedure)
                .WithOne(x => x.BillDetail)
                .HasForeignKey<BillDetail>(x => x.ClientProcedureId);
        }
    }
}
