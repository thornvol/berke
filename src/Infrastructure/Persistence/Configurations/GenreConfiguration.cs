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
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> entity)
        {
            // Configure inherited fields from BaseEntity
            BaseConfiguration.Configure(entity);

            // Table name
            entity.ToTable("Genres");

            // Primary Key GameId
            entity.HasKey(e => e.GenreId);

            //---------------------------
            // Name column
            entity.HasIndex(e => e.Name, "IX_Genres_Name").IsUnique().HasFilter("[Name] IS NOT NULL");

            // Set length to 256 (arbitrary) and don't allow nulls
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}
