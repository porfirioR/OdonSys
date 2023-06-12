using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utilities.Enums;
using Utilities.Extensions;

namespace Access.Sql.Configurations
{
    public class PermissionConfiguration : BaseEntityTypeConfiguration<Permission>
    {
        public override void Configure(EntityTypeBuilder<Permission> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => new { x.Name, x.RoleId });

            builder.Property(s => s.Name)
               .HasConversion(
                   s => s.GetDescription(),
                   s => (PermissionName)Enum.Parse(typeof(PermissionName), s));
        }
    }
}
