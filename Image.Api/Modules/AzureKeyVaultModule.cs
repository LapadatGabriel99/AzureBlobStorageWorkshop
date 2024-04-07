using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Image.Api.Services;
using Image.Api.Services.Contracts;

namespace Image.Api.Modules
{
    public static class AzureKeyVaultModule
    {
        public static IServiceCollection AddAzureKeyVault(this IServiceCollection services, IConfiguration configuration, bool isProductionEnvironment)
        {
            if (isProductionEnvironment)
            {
                services.AddSingleton(provider =>
                    new SecretClient(new Uri(configuration.GetValue<string>("AzureKeyVaultUri")), new DefaultAzureCredential()));
                services.AddSingleton<ISecretService, KeyVaultService>();

                return services;
            }

            services.AddSingleton<ISecretService, DefaultSecretService>();

            return services;
        }
    }
}
