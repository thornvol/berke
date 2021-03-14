using System;
using System.Threading.Tasks;
using BerkeGaming.Domain.Entities.Games;

namespace BerkeGaming.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Users
            var harry = new User {
                UserName = "harry", Password = "hedwig", IsAdministrator = true, CreatedDate = DateTimeOffset.UtcNow
            };
            var hermione = new User {
                UserName = "hermione",
                Password = "crookshanks",
                IsAdministrator = false,
                CreatedDate = DateTimeOffset.UtcNow
            };

            // Add Users to context
            await context.Users.AddRangeAsync(harry, hermione);

            // Genres
            var horror = new Genre {Name = "Horror", CreatedDate = DateTimeOffset.UtcNow};
            var adventure = new Genre {Name = "Adventure", CreatedDate = DateTimeOffset.UtcNow};
            var quest = new Genre {Name = "Quest", CreatedDate = DateTimeOffset.UtcNow};
            var coop = new Genre {Name = "Cooperative", CreatedDate = DateTimeOffset.UtcNow};
            var board = new Genre { Name = "Board Game", CreatedDate = DateTimeOffset.UtcNow};
            var fantasy = new Genre { Name = "Fantasy", CreatedDate = DateTimeOffset.UtcNow};
            var rpg = new Genre { Name = "RPG", CreatedDate = DateTimeOffset.UtcNow};
            var card = new Genre { Name = "Card Game", CreatedDate = DateTimeOffset.UtcNow};
            var digital = new Genre { Name = "Digital", CreatedDate = DateTimeOffset.UtcNow};
            var scifi = new Genre { Name = "Science Fiction", CreatedDate = DateTimeOffset.UtcNow };

            // Add Genres to context
            await context.Genres.AddRangeAsync(horror, adventure, quest, coop, board, fantasy, rpg, card, digital, scifi);

            // Publishers
            var asmodee = new Publisher {Name = "Asmodee Digital", CreatedDate = DateTimeOffset.UtcNow};
            var activision = new Publisher {Name = "Activision", CreatedDate = DateTimeOffset.UtcNow};
            var bethesda = new Publisher {Name = "Bethesda Softworks", CreatedDate = DateTimeOffset.UtcNow};
            var msft = new Publisher {Name = "Microsoft", CreatedDate = DateTimeOffset.UtcNow};
            await context.Publishers.AddRangeAsync(asmodee, activision, bethesda, msft);

            // Games
            var halo = new Game {
                Name = "Halo 1",
                Genres = new[] {scifi, digital, adventure},
                Publisher = msft,
                ReleaseDate = new DateTimeOffset(2001, 11, 15, 0, 0, 0, new TimeSpan(0, 0, 0)),
                Overview =
                    @"Halo is an American military science fiction media franchise managed and developed by 343 Industries and published by Xbox Game Studios. The franchise and its early main installments were originally developed by Bungie."
            };

            var doom = new Game
            {
                Name = "Doom Eternal",
                Genres = new[] { scifi, digital, adventure, horror },
                Publisher = bethesda,
                ReleaseDate = new DateTimeOffset(2020, 3, 20, 0, 0, 0, new TimeSpan(0, 0, 0)),
                Overview =
                    @"Doom Eternal is a first-person shooter game developed by id Software and published by Bethesda Softworks."
            };

            var gloomhaven = new Game
            {
                Name = "Gloomhaven Digital",
                Genres = new[] { fantasy, digital, adventure, coop },
                Publisher = asmodee,
                ReleaseDate = new DateTimeOffset(2019, 7, 1, 0, 0, 0, new TimeSpan(0, 0, 0)),
                Overview =
                    @"Gloomhaven, the digital adaptation of the acclaimed board game, mixes Tactical-RPG and dungeon-crawling. Its challenges, legendary for their unforgiving nature, reward only the most daring players with the sharpest minds. With your guild of fearless mercenaries, you will carve your way through terrifying dungeons, dreadful forests and dark caves filled with increasingly horrific monsters to reap the rewards…or die trying. Face the dangers of the world of Gloomhaven side by side with your friends in the online co-op mode."
            };

            // Add Games to context
            await context.Games.AddRangeAsync(halo, doom, gloomhaven);

            // User Games
            var harrysGames = new UserGame[] {
                new UserGame {User = harry, Game = halo}, new UserGame {User = harry, Game = gloomhaven}
            };

            var hermionesGames = new UserGame[] {
                new UserGame {User = hermione, Game = doom}, new UserGame {User = harry, Game = halo}
            };

            // Add UserGames to context
            await context.UserGames.AddRangeAsync(harrysGames);
            await context.UserGames.AddRangeAsync(hermionesGames);

            // Save changes to db
            await context.SaveChangesAsync();
        }
    }
}
