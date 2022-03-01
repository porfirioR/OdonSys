using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sql.Entities;

namespace Sql.Configurations
{
    public class ProcedureToothConfiguration : IEntityTypeConfiguration<ProcedureTooth>
    {
        public void Configure(EntityTypeBuilder<ProcedureTooth> builder)
        {
            builder
                .HasOne(x => x.Procedure)
                .WithMany(x => x.ProcedureTeeth)
                .HasForeignKey(x => x.ProcedureId);

            builder
                .HasOne(x => x.Tooth)
                .WithMany(x => x.ProcedureTeeth)
                .HasForeignKey(x => x.ToothId);

            builder.HasKey(x => new { x.ToothId, x.ProcedureId });
        }
    }
}
