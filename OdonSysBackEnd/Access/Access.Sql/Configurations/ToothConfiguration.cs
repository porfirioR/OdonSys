using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utilities.Enums;
using Utilities.Extensions;

namespace Access.Sql.Configurations
{
    public class ToothConfiguration : IEntityTypeConfiguration<Tooth>
    {
        public void Configure(EntityTypeBuilder<Tooth> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(d => d.DateCreated)
                .HasDefaultValueSql("GetDate()");

            builder
                .Property(d => d.DateModified)
                .HasDefaultValueSql("GetDate()");

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
}
