using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sql.Entities;

namespace Sql.Configurations
{
    public class DoctorClientConfiguration : IEntityTypeConfiguration<DoctorClient>
    {
        public void Configure(EntityTypeBuilder<DoctorClient> builder)
        {
            builder
                .HasOne(x => x.Doctor)
                .WithMany(x => x.DoctorsClients)
                .HasForeignKey(x => x.DoctorId);

            builder
                .HasOne(x => x.Client)
                .WithMany(x => x.DoctorsClients)
                .HasForeignKey(x => x.ClientId);

            builder.HasKey(x => new { x.DoctorId, x.ClientId });
        }
    }
}
