using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using BerkeGaming.Infrastructure.Identity;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BerkeGaming.Api.Filters
{
    /// <summary>
    /// Authenticates JWT token from Http Request.
    /// If user is valid, sets User on HttpContext.
    /// Modified from https://github.com/cuongle/WebApi.Jwt/blob/master/WebApi.Jwt/Filters/JwtAuthenticationAttribute.cs
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class JwtAuthenticate : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public JwtAuthenticate()
        {
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var request = context.HttpContext.Request;
            var authorization = AuthenticationHeaderValue.Parse(request.Headers["Authorization"]);

            if (authorization == null || authorization.Scheme.ToLower() != "bearer")
            {
                return;
            }

            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                context.Result = new UnauthorizedObjectResult(request);
                return;
            }

            var token = authorization.Parameter;
            var principal = await AuthenticateJwtToken(token);

            if (principal == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            //// If Role is passed-in, check that the user's Role Claim matches required Role Claim
            //if (!string.IsNullOrWhiteSpace(this.Roles))
            //{
            //    var roleExists = UserRoleExists(token, this.Roles);
            //    if (!roleExists)
            //    {
            //        context.Result = new ForbidResult();
            //        return;
            //    }
            //}

            context.HttpContext.User = principal as ClaimsPrincipal ??
                                       throw new InvalidOperationException(
                                           "Failed to cast principal to claims principal");
        }

        private static bool ValidateToken(string token, out string username)
        {
            username = null;

            var simplePrinciple = JwtTokenHelper.GetPrincipal(token);

            if (!(simplePrinciple?.Identity is ClaimsIdentity identity))
            {
                return false;
            }

            if (!identity.IsAuthenticated)
            {
                return false;
            }

            var usernameClaim = identity.FindFirst(ClaimTypes.Name);
            username = usernameClaim?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return false;
            }

            // More validate to check whether username exists in system

            return true;
        }

        /// <summary>
        /// Check User has claim that matches role.
        /// </summary>
        /// <param name="token">The User's Token</param>
        /// <param name="role">The Role to check against user's claims.</param>
        /// <returns></returns>
        private static bool UserRoleExists(string token, string role)
        {
            var simplePrinciple = JwtTokenHelper.GetPrincipal(token);
            var identity = simplePrinciple?.Identity as ClaimsIdentity;
            var roleClaim = identity?.FindFirst(ClaimTypes.Role);
            return string.Equals(roleClaim?.Value, role, StringComparison.InvariantCultureIgnoreCase);
        }

        protected async Task<IPrincipal> AuthenticateJwtToken(string token)
        {
            if (!ValidateToken(token, out var username))
            {
                return await Task.FromResult<IPrincipal>(null);
            }

            // based on username to get more information from database in order to build local identity
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, username)
                // Add more claims if needed: Roles, ...
            };

            var identity = new ClaimsIdentity(claims, "Jwt");
            var user = new ClaimsPrincipal(identity);

            return await Task.FromResult(user);
        }
    }
}
