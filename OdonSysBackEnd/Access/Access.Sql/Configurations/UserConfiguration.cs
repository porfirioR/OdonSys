using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utilities.Enums;
using Utilities.Extensions;

namespace Access.Sql.Configurations
{
    public class UserConfiguration : BaseEntityTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder
                .Property(x => x.Name)
                .HasMaxLength(30)
                .IsRequired();

            builder
                .Property(x => x.MiddleName)
                .HasMaxLength(30);

            builder
                .Property(x => x.Surname)
                .HasMaxLength(30)
                .IsRequired();

            builder
                .Property(x => x.SecondSurname)
                .HasMaxLength(30);

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
                .HasMaxLength(50)
                .IsRequired();

            builder
                .HasIndex(x => x.Document)
                .IsUnique();

            builder
                .HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Country)
               .HasConversion(
                   x => x.GetDescription(),
                   x => (Country)Enum.Parse(typeof(Country), x)
               );
        }
    }
}
