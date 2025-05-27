using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utilities.Enums;
using Utilities.Extensions;

namespace Access.Sql.Configurations;

internal sealed class ToothConfiguration : BaseEntityTypeConfiguration<Tooth>
{
    public override void Configure(EntityTypeBuilder<Tooth> builder)
    {
        base.Configure(builder);

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(30);

        builder
            .Property(x => x.Number)
            .IsRequired();

        builder
            .Property(x => x.Group)
            .IsRequired();

        builder.Property(s => s.Jaw)
           .HasConversion(
               s => s.GetDescription(),
               s => (Jaw)Enum.Parse(typeof(Jaw), s));

        builder.Property(s => s.Quadrant)
           .HasConversion(
               s => s.GetDescription(),
               s => (Quadrant)Enum.Parse(typeof(Quadrant), s));

        builder.Property(s => s.Group)
           .HasConversion(
               s => s.GetDescription(),
               s => (DentalGroup)Enum.Parse(typeof(DentalGroup), s));
    }
}
