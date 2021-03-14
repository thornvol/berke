using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BerkeGaming.Domain.Entities.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BerkeGaming.Infrastructure.Persistence.Configurations
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> entity)
        {
            // Configure inherited fields from BaseEntity
            BaseConfiguration.Configure(entity);

            // Table name
            entity.ToTable("Publishers");

            // Primary Key PublisherId
            entity.HasKey(e => e.PublisherId);

            //---------------------------
            // Name column
            entity.HasIndex(e => e.Name, "IX_Publishers_Name").IsUnique().HasFilter("[Name] IS NOT NULL").IsUnique();

            // Set length to 256 (arbitrary) and don't allow nulls
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}
