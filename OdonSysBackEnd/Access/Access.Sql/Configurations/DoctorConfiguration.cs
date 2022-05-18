using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Access.Sql.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.DateCreated)
                .HasDefaultValueSql("GetDate()");

            builder
                .Property(x => x.DateModified)
                .HasDefaultValueSql("GetDate()");

            builder
                .Property(x => x.Name)
                .HasMaxLength(25);

            builder
                .Property(x => x.LastName)
                .HasMaxLength(25);

            builder
                .Property(x => x.Document)
                .HasMaxLength(15);

            builder
                .HasIndex(x => x.Document)
                .IsUnique();

            builder.HasOne(x => x.User)
                .WithOne(x => x.Doctor)
                .HasForeignKey<User>(x => x.Id);
        }
    }
}
