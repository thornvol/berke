using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BerkeGaming.Application.Common.Interfaces;
using BerkeGaming.Application.Common.Models;
using BerkeGaming.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BerkeGaming.Api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Produces("application/json")]
    [Route("")]
    public class SecurityController : ApiControllerBase
    {
        public SecurityController()
        {
            
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
