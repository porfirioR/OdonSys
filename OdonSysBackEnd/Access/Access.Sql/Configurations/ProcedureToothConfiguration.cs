using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sql.Entities;

namespace Sql.Configurations
{
    public class ProcedureToothConfiguration : IEntityTypeConfiguration<ProcedureTooth>
    {
        public void Configure(EntityTypeBuilder<ProcedureTooth> builder)
        {

            builder.HasKey(x => x.Id);

            builder
                .Property(d => d.DateCreated)
                .HasDefaultValueSql("GetDate()");

            builder
                .Property(d => d.DateModified)
                .HasDefaultValueSql("GetDate()");

            builder
                .HasOne(x => x.Procedure)
                .WithMany(x => x.ProcedureTeeth)
                .HasForeignKey(x => x.ProcedureId);

            builder
                .HasOne(x => x.Tooth)
                .WithMany(x => x.ProcedureTeeth)
                .HasForeignKey(x => x.ToothId);

            builder.HasIndex(x => new { x.ToothId, x.ProcedureId });
        }
    }
}
