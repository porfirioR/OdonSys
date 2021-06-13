using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sql.Entities;

namespace Sql.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .Property(d => d.DateCreated)
                .HasDefaultValueSql("GetUtcDate()");

            builder
                .Property(d => d.DateModified)
                .HasDefaultValueSql("GetUtcDate()");

            builder
                .Property(d => d.Name)
                .HasMaxLength(25);
        }
    }
}
