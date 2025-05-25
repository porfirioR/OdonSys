using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Access.Sql.Configurations
{
    public class OrthodonticConfiguration : BaseEntityTypeConfiguration<Orthodontic>
    {
        public override void Configure(EntityTypeBuilder<Orthodontic> builder)
        {
            base.Configure(builder);

            builder
                .Property(x => x.Date)
                .IsRequired();

            builder
                .Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasOne(x => x.Client)
                .WithMany(x => x.Orthodontics)
                .HasForeignKey(x => x.ClientId);
        }
    }
}
