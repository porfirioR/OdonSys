using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Access.Sql.Configurations;

public abstract class BaseEntityTypeConfiguration<TBase> : IEntityTypeConfiguration<TBase>
where TBase : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TBase> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.DateCreated)
            .HasDefaultValueSql("GetDate()");

        builder
            .Property(x => x.DateModified)
            .HasDefaultValueSql("GetDate()");
    }
}
