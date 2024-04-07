using Azure.Security.KeyVault.Secrets;
using Image.Api.Services.Contracts;

namespace Image.Api.Services
{
    public class KeyVaultService : ISecretService
    {
        private readonly SecretClient _secretClient;

        public KeyVaultService(SecretClient secretClient)
        {
            _secretClient = secretClient;
        }

        public async Task<string> GetSecret(string secretName)
        {
            var secret = await _secretClient.GetSecretAsync(secretName);

            if (secret.GetRawResponse().IsError)
            {
                throw new Exception($"Unable to retrieve secret {secretName}!");
            };

            return secret.Value.Value;
        }
    }
}
