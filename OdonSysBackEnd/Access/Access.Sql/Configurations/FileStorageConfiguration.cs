using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Access.Sql.Configurations
{
    internal class FileStorageConfiguration : BaseEntityTypeConfiguration<FileStorage>
    {
        public override void Configure(EntityTypeBuilder<FileStorage> builder)
        {
            base.Configure(builder);

            builder
                .Property(x => x.Url)
                .IsRequired();

            builder
                .Property(x => x.ReferenceId)
                .IsRequired();
        }
    }
}
