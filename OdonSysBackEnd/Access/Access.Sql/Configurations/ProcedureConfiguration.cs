using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Access.Sql.Configurations
{
    public class ProcedureConfiguration : IEntityTypeConfiguration<Procedure>
    {
        public void Configure(EntityTypeBuilder<Procedure> builder)
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
                .HasMaxLength(30);

            builder
                .Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(x => x.Price)
                .IsRequired();
        }
    }
}
