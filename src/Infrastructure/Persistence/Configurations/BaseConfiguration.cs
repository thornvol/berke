using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BerkeGaming.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BerkeGaming.Infrastructure.Persistence.Configurations
{
    public static class BaseConfiguration
    {
        public static void Configure<TEntity>(EntityTypeBuilder<TEntity> entity) where TEntity : BaseEntity
        {
            // Default created date to now
            entity.Property(e => e.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("(getutcdate())");
        }
    }
}
