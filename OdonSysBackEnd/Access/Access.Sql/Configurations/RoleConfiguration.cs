using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Access.Sql.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .Property(d => d.DateCreated)
                .HasDefaultValueSql("GetDate()");

            builder
                .Property(d => d.DateModified)
                .HasDefaultValueSql("GetDate()");

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
