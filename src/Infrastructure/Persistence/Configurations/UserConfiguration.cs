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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            // Configure inherited fields from BaseEntity
            BaseConfiguration.Configure(entity);

            // Table name
            entity.ToTable("Users");

            // Primary Key Id
            entity.HasKey(e => e.Id);

            // User Name
            entity.Property(e => e.UserName)
                .HasMaxLength(256)
                .IsRequired();

            entity.HasIndex(e => e.UserName, "IX_Users_UserName")
                .IsUnique();

            // Password
            entity.Property(e => e.Password)
                .HasMaxLength(128);
        }
    }
}
