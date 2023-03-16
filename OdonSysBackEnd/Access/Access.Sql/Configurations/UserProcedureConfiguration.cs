﻿using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Access.Sql.Configurations
{
    public class UserProcedureConfiguration : IEntityTypeConfiguration<UserProcedure>
    {
        public void Configure(EntityTypeBuilder<UserProcedure> builder)
        {
            builder.HasKey(x => new { x.Id, x.ProcedureId, x.UserId });

            builder
                .Property(d => d.DateCreated)
                .HasDefaultValueSql("GetDate()");

            builder
                .Property(d => d.DateModified)
                .HasDefaultValueSql("GetDate()");

            builder
                .Property(x => x.Price)
                .IsRequired();
        }
    }
}