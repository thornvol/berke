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
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> entity)
        {
            // Configure inherited fields from BaseEntity
            BaseConfiguration.Configure(entity);

            // Table name
            entity.ToTable("Games");

            // Primary Key GameId
            entity.HasKey(e => e.GameId);

            // Name column
            entity.HasIndex(e => e.Name, "IX_Games_Name").IsUnique().HasFilter("[Name] IS NOT NULL").IsUnique();
            
            // Set length to 256 (arbitrary) and don't allow nulls
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .IsRequired();

            // Release Date column
            entity.HasIndex(e => e.ReleaseDate, "IX_Games_ReleaseDate");

            // Set don't allow nulls
            entity.Property(e => e.ReleaseDate)
                .IsRequired();

            // Overview column - Set don't allow nulls
            entity.Property(e => e.Overview)
                .HasMaxLength(4000)
                .IsRequired();

            
            // Foreign Keys

            // Many-to-many Games <--> Genres using EF 5 new implicit join table creation
            // Join table has name "GameGenres"
            // https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-composite-key%2Csimple-key#many-to-many)
            entity.HasMany(g => g.Genres).WithMany(g => g.Games).UsingEntity(j => j.ToTable("GameGenres"));

            // One-to-many Games <--> Publishers
            entity.HasOne(g => g.Publisher).WithMany(p => p.Games).HasForeignKey(g => g.PublisherId);
        }
    }
}
