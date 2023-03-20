using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Access.Sql.Configurations
{
    public class UserClientConfiguration : IEntityTypeConfiguration<UserClient>
    {
        public void Configure(EntityTypeBuilder<UserClient> builder)
        {
            builder
                .HasOne(x => x.User)
                .WithMany(x => x.UserClients)
                .HasForeignKey(x => x.UserId);

            builder
                .HasOne(x => x.Client)
                .WithMany(x => x.UserClients)
                .HasForeignKey(x => x.ClientId);

            builder.HasKey(x => new { x.UserId, x.ClientId });
        }
    }
}
