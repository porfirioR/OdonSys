﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sql.Entities;

namespace Sql.Configurations
{
    public class ProcedureConfiguration : IEntityTypeConfiguration<Procedure>
    {
        public void Configure(EntityTypeBuilder<Procedure> builder)
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
                .IsRequired()
                .HasMaxLength(30);

            builder
                .Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(x => x.EstimatedSessions)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}