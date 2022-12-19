using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Access.Sql.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.DateCreated)
                .HasDefaultValueSql("GetDate()");

            builder
                .Property(x => x.DateModified)
                .HasDefaultValueSql("GetDate()");
            
            // custom Properties
            builder
                .Property(x => x.Name)
                .HasMaxLength(25)
                .IsRequired();

            builder
                .Property(x => x.MiddleName)
                .HasMaxLength(25);

            builder
                .Property(x => x.LastName)
                .HasMaxLength(25)
                .IsRequired();

            builder
                .Property(x => x.MiddleLastName)
                .HasMaxLength(25);

            builder
                .Property(x => x.Document)
                .HasMaxLength(15)
                .IsRequired();

            builder
                .Property(x => x.Phone)
                .HasMaxLength(15)
                .IsRequired();

            builder
                .Property(x => x.Email)
                .HasMaxLength(20)
                .IsRequired();

            builder
                .HasIndex(x => x.Document)
                .IsUnique();

            builder
                .HasIndex(x => x.Email)
                .IsUnique();

        }
    }
}
