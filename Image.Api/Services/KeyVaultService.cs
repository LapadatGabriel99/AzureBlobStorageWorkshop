using Azure.Security.KeyVault.Secrets;
using Image.Api.Services.Contracts;

namespace Image.Api.Services
{
    public class KeyVaultService : ISecretService
    {
        private readonly SecretClient _secretClient;
        private readonly IConfiguration _configuration;

        public KeyVaultService(SecretClient secretClient, IConfiguration configuration)
        {
            _secretClient = secretClient;
            _configuration = configuration;
        }

        public async Task<string> GetSecret(string secretName)
        {
            var secret = await _secretClient.GetSecretAsync(_configuration.GetValue<string>(secretName));

            if (secret.GetRawResponse().IsError)
            {
                throw new Exception($"Unable to retrieve secret {secretName}!");
            };

            return secret.Value.Value;
        }
    }
}
