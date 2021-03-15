using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace BerkeGaming.Infrastructure.Identity
{
    public static class JwtTokenHelper
    {
        public const string Secret =
            "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";

        /// <summary>
        /// Generate JWT with appropriate claims.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="expireMinutes"></param>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        public static string GenerateToken(string username, int expireMinutes = 20, bool isAdmin = false)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            // Create list of claims
            var claims = new List<Claim> {new(ClaimTypes.Name, username)};

            // If user is admin, then add Admin Role Claim 
            // TODO: Usually, this would live in a UserRole table/entity
            if (isAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            }

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var sToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(sToken);

            return token;
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                if (!(tokenHandler.ReadToken(token) is JwtSecurityToken jwtToken))
                {
                    return null;
                }

                var symmetricKey = Convert.FromBase64String(Secret);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

                return principal;
            }

            catch (Exception)
            {
                return null;
            }
        }
    }
}
