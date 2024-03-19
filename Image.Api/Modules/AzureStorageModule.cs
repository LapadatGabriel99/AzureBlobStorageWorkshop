using Azure.Identity;
using Azure.Storage.Blobs;

namespace Image.Api.Modules
{
    public static class AzureStorageModule
    {
        public static IServiceCollection AddAzureStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(
                provider => new BlobServiceClient(
                    new Uri(configuration.GetValue<string>("AzureBlobStorageUri")),
                    new DefaultAzureCredential()));

            return services;
        }
    }
}
