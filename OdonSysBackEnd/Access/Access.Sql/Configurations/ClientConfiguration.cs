using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Access.Sql.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(d => d.DateCreated)
                .HasDefaultValueSql("GetDate()");

            builder
                .Property(d => d.DateModified)
                .HasDefaultValueSql("GetDate()");

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

        }
    }
}
