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
    public class UserGameConfiguration : IEntityTypeConfiguration<UserGame>
    {
        public void Configure(EntityTypeBuilder<UserGame> entity)
        {
            // Configure inherited fields from BaseEntity
            BaseConfiguration.Configure(entity);

            // Table name
            entity.ToTable("UserGames");

            // Primary Composite Key UserId + GameId
            entity.HasKey(e => new {e.UserId, e.GameId});

            //// Index
            //entity.HasIndex(e => e.GameId, "IX_UserGame_GameId");
            //entity.HasIndex(e => e.UserId, "IX_UserGame_UserId");

            // Foreign Keys (Game and User will have no navigation property back to UserGame --> an assumption)
            // Cascading deletes to prevent orphaned records

            entity.HasOne(ug => ug.Game).WithMany().HasForeignKey(ug => ug.GameId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(ug => ug.User).WithMany().HasForeignKey(ug => ug.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            //entity.HasOne(ug => ug.Game).WithOne().HasForeignKey<Game>(g => g.GameId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //entity.HasOne(ug => ug.User).WithOne().HasForeignKey<User>(u => u.Id)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
