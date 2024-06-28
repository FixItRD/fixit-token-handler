using fixit_token_handler.Models;
using fixit_token_handler.Services.Templates;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace fixit_token_handler.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly TokenConfiguration _configuration;

        public JwtService(TokenConfiguration jwtConfiguration)
        {
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            _configuration = jwtConfiguration;
        }

        public string GenerateJwtToken(List<Claim> payloadContents)
        {
            var signingCredentials = new SigningCredentials(_configuration.SecurityKey, "HS256");

            IEnumerable<Claim> payloadClaims = payloadContents;

            var securityToken = new JwtSecurityToken(
                    issuer: _configuration.Issuer,
                    audience: _configuration.Audience,
                    claims: payloadClaims,
                    signingCredentials: signingCredentials,
                    expires: DateTime.Now.AddDays(1)
                );

            return _jwtSecurityTokenHandler.WriteToken(securityToken);
        }

        public UserClaimsModel GetUserClaims(ClaimsPrincipal user)
        {
            return new UserClaimsModel
            {
                Name = user?.FindFirstValue(ClaimTypes.Name),
                Role = user?.FindFirstValue(ClaimTypes.Role)
            };
        }
    }
}
