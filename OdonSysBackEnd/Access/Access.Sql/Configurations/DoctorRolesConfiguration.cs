using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sql.Entities;

namespace Sql.Configurations
{
    public class DoctorRolesConfiguration : IEntityTypeConfiguration<DoctorRoles>
    {
        public void Configure(EntityTypeBuilder<DoctorRoles> builder)
        {
            builder
                .HasOne(x => x.Doctor)
                .WithMany(x => x.DoctorRoles)
                .HasForeignKey(x => x.DoctorId);

            builder
                .HasOne(x => x.Role)
                .WithMany(x => x.DoctorRoles)
                .HasForeignKey(x => x.RolId);

            builder.HasKey(x => new { x.DoctorId, x.RolId });
        }
    }
}
