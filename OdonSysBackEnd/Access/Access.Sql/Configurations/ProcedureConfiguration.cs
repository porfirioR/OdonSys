using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Access.Sql.Configurations;

internal sealed class ProcedureConfiguration : BaseEntityTypeConfiguration<Procedure>
{
    public override void Configure(EntityTypeBuilder<Procedure> builder)
    {
        base.Configure(builder);

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(60);

        builder
            .Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(x => x.Price)
            .IsRequired();
    }
}
