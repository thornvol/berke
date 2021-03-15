using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BerkeGaming.Application.Common.Models;
using BerkeGaming.Domain.Entities.Games;

namespace BerkeGaming.Application.Common.Interfaces
{
    public interface IApplicationDbRepository
    {
        /// <summary>
        /// Validate user's password.
        /// </summary>
        /// <param name="username">The User Name.</param>
        /// <param name="password">The User's password.</param>
        /// <returns></returns>
        Task<bool> ValidatePassword(string username, string password);

        /// <summary>
        /// Retrieve all games for the given user id.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<IQueryable<Game>> GetGamesForUser(string userName);

        /// <summary>
        /// Add Game to User's collection of games.
        /// </summary>
        /// <param name="gameId">The Game's Id.</param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<Result> AddGameToUser(int gameId, string userName);

        /// <summary>
        /// Delete game from User's collection.
        /// </summary>
        /// <param name="gameId">Them Game id to delete.</param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<Result> DeleteGameForUser(int gameId, string userName);

        /// <summary>
        /// Add Game to game collection. User must be an administrator.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        Task<Result> AddGame(string userName, Game game);

        /// <summary>
        /// Get User by Id.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<User> GetUserByName(string userName);

        /// <summary>
        /// Get a list of Genre's by Ids.
        /// </summary>
        /// <param name="ids">A list of genre ids.</param>
        /// <returns></returns>
        Task<ICollection<Genre>> GetGenresById(IEnumerable<int> ids);

        /// <summary>
        /// Get Publisher By Id.
        /// </summary>
        /// <param name="publisherId"></param>
        /// <returns></returns>
        Task<Publisher> GetPublisherById(int publisherId);
    }
}
