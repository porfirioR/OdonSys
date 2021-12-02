using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sql.Entities;

namespace Sql.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(d => d.DateCreated)
                .HasDefaultValueSql("GetUtcDate()");

            builder
                .Property(d => d.DateModified)
                .HasDefaultValueSql("GetUtcDate()");

            builder
                .Property(x => x.Name)
                .HasMaxLength(25);

            builder
                .Property(x => x.SecondName)
                .HasMaxLength(25);

            builder
                .Property(x => x.LastName)
                .HasMaxLength(25);

            builder
                .Property(x => x.SecondLastName)
                .HasMaxLength(25);

            builder
                .HasIndex(x => x.Document)
                .IsUnique();

            builder
                .Property(x => x.Document)
                .HasMaxLength(15);

            builder
                .Property(x => x.Ruc)
                .HasMaxLength(1);

            builder
                .Property(x => x.Phone)
                .HasMaxLength(10);
        }
    }
}
