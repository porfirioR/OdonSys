using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Access.Sql.Configurations
{
    public class ClientProcedureConfiguration : IEntityTypeConfiguration<ClientProcedure>
    {
        public void Configure(EntityTypeBuilder<ClientProcedure> builder)
        {
            builder.HasKey(x => new { x.Id, x.ProcedureId, x.UserClientId });

            builder
                .Property(d => d.DateCreated)
                .HasDefaultValueSql("GetDate()");

            builder
                .Property(d => d.DateModified)
                .HasDefaultValueSql("GetDate()");

            builder
                .Property(x => x.Price)
                .IsRequired();

            builder
                .HasOne(x => x.UserClient)
                .WithMany(x => x.ClientProcedures)
                .HasForeignKey(x => x.UserClientId);

            builder
                .HasOne(x => x.Procedure)
                .WithMany(x => x.ClientProcedures)
                .HasForeignKey(x => x.ProcedureId);
        }
    }
}
