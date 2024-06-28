using fixit_token_handler.Services.Templates;

namespace fixit_token_handler.Services
{
    public class ServiceHandler : IServiceHandler
    {
        public IJwtService _jwtService { get; }

        public ServiceHandler(TokenConfiguration tokenConfiguration)
        {
            _jwtService = new JwtService(tokenConfiguration);
        }
    }
}
