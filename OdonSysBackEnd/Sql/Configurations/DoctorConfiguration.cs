using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sql.Entities;

namespace Sql.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
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

            builder
                .Property(d => d.LastName)
                .HasMaxLength(25);

            builder
                .Property(d => d.Document)
                .HasMaxLength(15);

            builder
                .HasIndex(d => d.Document)
                .IsUnique();
        }
    }
}
