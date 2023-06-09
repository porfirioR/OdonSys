using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utilities.Enums;
using Utilities.Extensions;

namespace Access.Sql.Configurations
{
    public class ClientConfiguration : BaseEntityTypeConfiguration<Client>
    {
        public override void Configure(EntityTypeBuilder<Client> builder)
        {
            base.Configure(builder);

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(25);

            builder
                .Property(x => x.MiddleName)
                .HasMaxLength(25);

            builder
                .Property(x => x.Surname)
                .IsRequired()
                .HasMaxLength(25);

            builder
                .Property(x => x.SecondSurname)
                .HasMaxLength(25);

            builder
                .HasIndex(x => x.Document)
                .IsUnique();

            builder
                .Property(x => x.Document)
                .IsRequired()
                .HasMaxLength(15);

            builder
                .Property(x => x.Ruc)
                .HasMaxLength(1);

            builder
                .Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(10);

            builder
                .Property(x => x.Country)
                .IsRequired();

            builder
                .Property(x => x.Email)
                .HasMaxLength(25);

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
