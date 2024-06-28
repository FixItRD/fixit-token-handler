using fixit_token_handler.Models;
using System.Security.Claims;

namespace fixit_token_handler.Services.Templates
{
    public interface IJwtService
    {
        public string GenerateJwtToken(List<Claim> payloadContents);

        public UserClaimsModel GetUserClaims(ClaimsPrincipal user);
    }
}
