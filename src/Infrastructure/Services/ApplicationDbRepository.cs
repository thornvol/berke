using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BerkeGaming.Application.Common.Exceptions;
using BerkeGaming.Application.Common.Interfaces;
using BerkeGaming.Application.Common.Models;
using BerkeGaming.Domain.Entities.Games;
using Microsoft.EntityFrameworkCore;

namespace BerkeGaming.Infrastructure.Services
{
    public class ApplicationDbRepository : IApplicationDbRepository
    {
        private readonly IApplicationDbContext _context;
        private readonly IUnityOfWork _unityOfWork;

        public ApplicationDbRepository(IApplicationDbContext context, IUnityOfWork unityOfWork)
        {
            _context = context;
            _unityOfWork = unityOfWork;
        }

        /// <summary>
        /// Validate user's password.
        /// </summary>
        /// <param name="username">The User Name.</param>
        /// <param name="password">The User's password.</param>
        /// <returns></returns>
        public async Task<bool> ValidatePassword(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException(nameof(username), "Must provide user name");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException(nameof(password), "Must provide user's password");
            }

            var user = await _context.Users?.AsNoTracking()?.Where(u => u.UserName == username)?.FirstOrDefaultAsync();
            if (user == null)
            {
                throw new NotFoundException($"User name {username} could not be found.");
            }

            return user.Password == password;
        }

        /// <summary>
        /// Retrieve all games for the given user id.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<IQueryable<Game>> GetGamesForUser(string username)
        {
            // Get user
            var user = await _context.Users.AsNoTracking().Where(u => u.UserName == username).FirstOrDefaultAsync();

            // If user is not found, throw exception
            if (user == null)
            {
                throw new NotFoundException($"User name '{username}' could not be found.");
            }

            return _context.UserGames?.AsNoTracking()?.Where(ug => ug.User == user)?.Select(ug => ug.Game);
        }

        /// <summary>
        /// Add Game to User's collection of games.
        /// </summary>
        /// <param name="gameId">The Game's Id.</param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<Result> AddGameToUser(int gameId, string userName)
        {
            // Get user
            var user = await GetUserByName(userName);

            // If user is not found, throw exception
            if (user == null)
            {
                throw new NotFoundException($"User name '{userName}' could not be found.");
            }

            // Get game
            var game = await GetGameById(gameId);

            // If game is not found, throw exception
            if (game == null)
            {
                throw new NotFoundException($"Game Id {gameId} could not be found.");
            }

            // If User already has game in their collection, throw exception
            if (await _context.UserGames.AsNoTracking().Where(ug => ug.Game == game && ug.User == user).AnyAsync())
            {
                throw new Exception($"Game Id {gameId} is already part of the user's collection.");
            }

            // Create UserGame entity
            var userGame = new UserGame {Game = game, User = user, CreatedDate = DateTimeOffset.UtcNow};

            // Add to the context
            await this._context.UserGames.AddAsync(userGame);

            // Save changes to db
            await _unityOfWork.Commit();

            return Result.Success();
        }

        /// <summary>
        /// Delete game from User's collection.
        /// </summary>
        /// <param name="gameId">Them Game id to delete.</param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<Result> DeleteGameForUser(int gameId, string userName)
        {
            // Get user
            var user = await GetUserByName(userName);

            // If user is not found, throw exception
            if (user == null)
            {
                throw new NotFoundException($"User name '{userName}' could not be found.");
            }

            // Get game
            var game = await GetGameById(gameId);

            // If game is not found, throw exception
            if (game == null)
            {
                throw new NotFoundException($"Game Id {gameId} could not be found.");
            }

            var userGame = await _context.UserGames.Where(ug => ug.Game == game).FirstOrDefaultAsync();
            if (userGame == null)
            {
                throw new NotFoundException($"Game {game.Name} is not part of User's collection.");
            }

            // remove from context
            _context.UserGames.Remove(userGame);

            // commit changes
            await _unityOfWork.Commit();

            return Result.Success();
        }

        /// <summary>
        /// Add Game to game collection. User must be an administrator.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public async Task<Result> AddGame(string userName, Game game)
        {
            // Get user
            var user = await GetUserByName(userName);

            // If user is not found, throw exception
            if (user == null)
            {
                throw new NotFoundException($"User name '{userName}' could not be found.");
            }

            if (!user.IsAdministrator)
            {
                throw new UnauthorizedAccessException($"User {user.UserName} is not authorized to add games.");
            }

            // Add Game
            await _context.Games.AddAsync(game);

            // Commit changes
            await _unityOfWork.Commit();

            return Result.Success();
        }

        /// <summary>
        /// Get User by Id.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<User> GetUserByName(string userName)
        {
            return await _context.Users.AsNoTracking().Where(u => u.UserName == userName.Trim()).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get Game by Id.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task<Game> GetGameById(int gameId)
        {
            return await _context.Games.AsNoTracking().Where(u => u.GameId == gameId).FirstOrDefaultAsync();
        }
    }
}
