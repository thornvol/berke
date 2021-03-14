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
        Task<bool> ValidatePassword(string username, string password);
        Task<IQueryable<Game>> GetGamesForUser(string userName);
        Task<Result> AddGameToUser(int gameId, string userName);
        Task<Result> DeleteGameForUser(int gameId, string userName);
        Task<Result> AddGame(string userName, Game game);
    }
}
