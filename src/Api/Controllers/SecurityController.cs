using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BerkeGaming.Application.Common.Interfaces;
using BerkeGaming.Application.Common.Models;
using BerkeGaming.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BerkeGaming.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("")]
    public class SecurityController : ApiControllerBase
    {
        private readonly ILogger<SecurityController> _logger;

        public SecurityController(ILogger<SecurityController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("/auth")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetUserToken([FromForm] UserLogin login,
            [FromServices] IApplicationDbRepository repository)
        {
            // Validate password
            if (!await repository.ValidatePassword(login.UserName, login.Password))
            {
                return Unauthorized();
            }

            var user = await repository.GetUserByName(login.UserName);

            // Generate token that expires in a year
            var jwt = JwtTokenHelper.GenerateToken(login.UserName, 525600, user?.IsAdministrator ?? false);

            // wrap toke as an anonymous object
            return Ok(new {value = jwt});
        }
    }
}
