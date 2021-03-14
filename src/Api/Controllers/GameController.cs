using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BerkeGaming.Api.Filters;
using BerkeGaming.Application.Common.Models;
using BerkeGaming.Application.Games.Commands.AddGameToUser;
using BerkeGaming.Application.Games.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BerkeGaming.Api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Produces("application/json")]
    [Route("")]
    public class GameController : ApiControllerBase
    {
        public GameController()
        {
            
        }

        /// <summary>
        /// Gets game for user.
        /// Demonstrates MediatR, too.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [JwtAuthenticate]
        [HttpGet("collection")]
        [ProducesResponseType(200, Type = typeof(IList<GameDto>))]
        [ProducesResponseType(422)]
        public async Task<IActionResult> GetGames([FromQuery] GetGamesQuery query)
        {
            var task = Mediator.Send(query);

            return await ExecuteTaskHandler(task);
        }

        /// <summary>
        /// Add Game to User's collection.
        /// </summary>
        /// <param name="command">A request including the game to add.</param>
        /// <returns></returns>
        [JwtAuthenticate]
        [HttpPost("collection")]
        [ProducesResponseType(201, Type = typeof(IList<GameDto>))]
        [ProducesResponseType(422)]
        public async Task<IActionResult> AddGameToUser([FromBody] AddGameToUserCommand command)
        {
            var task = Mediator.Send(command);

            return await ExecuteTaskHandler(task);
        }
    }
}
