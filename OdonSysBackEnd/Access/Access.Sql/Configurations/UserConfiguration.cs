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
                .HasMaxLength(25)
                .IsRequired();

            builder
                .Property(x => x.MiddleName)
                .HasMaxLength(25);

            builder
                .Property(x => x.Surname)
                .HasMaxLength(25)
                .IsRequired();

            builder
                .Property(x => x.SecondSurname)
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

            builder.Property(s => s.Country)
               .HasConversion(
                   s => s.GetDescription(),
                   s => (Country)Enum.Parse(typeof(Country), s));
        }
    }
}
