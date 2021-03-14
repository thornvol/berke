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
        public async Task<IActionResult> GetUserToken([FromForm] UserLogin login,
            [FromServices] IApplicationDbRepository repository)
        {
            // Validate password
            if (!await repository.ValidatePassword(login.UserName, login.Password))
            {
                return Unauthorized();
            }

            var jwt = JwtTokenHelper.GenerateToken(login.UserName);
            return Ok(jwt);
        }
    }
}
