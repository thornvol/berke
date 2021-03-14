using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BerkeGaming.Api.Filters;
using BerkeGaming.Application.Common.Models;
using BerkeGaming.Application.Games.Commands.AddGame;
using BerkeGaming.Application.Games.Commands.AddGameToUser;
using BerkeGaming.Application.Games.Commands.DeleteGameForUser;
using BerkeGaming.Application.Games.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BerkeGaming.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("")]
    public class GameController : ApiControllerBase
    {
        private readonly ILogger<GameController> _logger;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
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
        /// <param name="command">A request including the Id of the Game to add.</param>
        /// <returns></returns>
        [JwtAuthenticate]
        [HttpPost("collection")]
        [ProducesResponseType(204)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> AddGameToUser([FromBody] AddGameToUserCommand command)
        {
            var result = await Mediator.Send(command);

            return result ? NoContent() : UnprocessableEntity();
        }

        /// <summary>
        /// Delete Game from User's collection.
        /// </summary>
        /// <param name="command">A request including the game id to delete.</param>
        /// <returns></returns>
        [JwtAuthenticate]
        [HttpDelete("collection")]
        [ProducesResponseType(204)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> DeleteGameForUser([FromRoute] DeleteGameForUserCommand command)
        {
            var result = await Mediator.Send(command);

            return result ? NoContent() : UnprocessableEntity();
        }

        /// <summary>
        /// Add Game to Games Collection.
        /// </summary>
        /// <param name="command">A request including the game to add.</param>
        /// <returns></returns>
        [JwtAuthenticate]
        [HttpPost("games")]
        [ProducesResponseType(201)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> AddGame([FromBody] AddGameCommand command)
        {
            var result = await Mediator.Send(command);

            return result ? CreatedAtAction(nameof(AddGame), true) : UnprocessableEntity();
        }
    }
}
