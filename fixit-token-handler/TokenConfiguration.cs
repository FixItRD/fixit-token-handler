using Microsoft.IdentityModel.Tokens;

namespace fixit_token_handler
{
    public class TokenConfiguration
    {
        public TokenConfiguration(string issuer, string audience, SymmetricSecurityKey securityKey)
        {
            Issuer = issuer;
            Audience = audience;
            SecurityKey = securityKey;
        }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public SymmetricSecurityKey SecurityKey { get; set; }
    }
}
