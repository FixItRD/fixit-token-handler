using fixit_token_handler.Models;
using fixit_token_handler.Services.Templates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace fixit_token_handler.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        private readonly ILogger<TokenController> _logger;
        private readonly IServiceHandler _serviceHandler;

        public TokenController(ILogger<TokenController> logger, IServiceHandler serviceHandler)
        {
            _logger = logger;
            _serviceHandler = serviceHandler;
        }

        [HttpPost(Name = "GenerateToken")]
        public string Get(UserClaimsModel claims)
        {
            _logger.LogInformation($"Generating token for user {claims.UserId}");
            return _serviceHandler._jwtService.GenerateJwtToken(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, claims.UserId.ToString()),
                new Claim(ClaimTypes.Name, claims.Name),
                new Claim(ClaimTypes.Role, claims.Role),
            });
        }
    }
}
