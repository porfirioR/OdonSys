using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Access.Sql.Configurations
{
    public class ClientProcedureConfiguration : BaseEntityTypeConfiguration<ClientProcedure>
    {
        public override void Configure(EntityTypeBuilder<ClientProcedure> builder)
        {
            base.Configure(builder);

            builder
                .HasIndex(x => new { x.Id, x.ProcedureId, x.UserClientId })
                .IsUnique();

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
