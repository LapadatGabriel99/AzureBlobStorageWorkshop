using Image.Api.Services.Contracts;
using Microsoft.Extensions.Configuration;

namespace Image.Api.Services
{
    public class DefaultSecretService : ISecretService
    {
        private readonly IConfiguration _configuration;

        public DefaultSecretService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetSecret(string secretName)
        {
            return _configuration.GetValue<string>(secretName);
        }
    }
}
