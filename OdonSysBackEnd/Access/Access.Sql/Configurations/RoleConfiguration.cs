using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sql.Entities;

namespace Sql.Configurations
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
                .HasMaxLength(25);
        }
    }
}
