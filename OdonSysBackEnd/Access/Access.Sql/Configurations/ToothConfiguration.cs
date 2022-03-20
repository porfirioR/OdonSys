using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sql.Entities;

namespace Sql.Configurations
{
    public class ToothConfiguration : IEntityTypeConfiguration<Tooth>
    {
        public void Configure(EntityTypeBuilder<Tooth> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(d => d.DateCreated)
                .HasDefaultValueSql("GetUtcDate()");

            builder
                .Property(d => d.DateModified)
                .HasDefaultValueSql("GetUtcDate()");

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

            //builder.HasKey(x => new { x.Group, x.Name, x.Number });
        }
    }
}
