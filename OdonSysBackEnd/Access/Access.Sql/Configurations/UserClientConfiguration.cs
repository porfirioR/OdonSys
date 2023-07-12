using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Access.Sql.Configurations
{
    public class UserClientConfiguration : BaseEntityTypeConfiguration<UserClient>
    {
        public override void Configure(EntityTypeBuilder<UserClient> builder)
        {
            base.Configure(builder);
            builder
                .HasIndex(x => new { x.Id, x.UserId, x.ClientId })
                .IsUnique();

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.UserClients)
                .HasForeignKey(x => x.UserId);

            builder
                .HasOne(x => x.Client)
                .WithMany(x => x.UserClients)
                .HasForeignKey(x => x.ClientId);
        }
    }
}
