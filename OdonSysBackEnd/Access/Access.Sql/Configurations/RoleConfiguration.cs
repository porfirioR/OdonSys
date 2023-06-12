using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Access.Sql.Configurations
{
    public class RoleConfiguration : BaseEntityTypeConfiguration<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);

            builder
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(25);

            builder
                .Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .HasIndex(x => x.Name)
                .IsUnique();

            builder
                .HasIndex(x => x.Code)
                .IsUnique();

            builder
                .HasMany(x => x.RolePermissions)
                .WithOne(x => x.Role);
        }
    }
}
