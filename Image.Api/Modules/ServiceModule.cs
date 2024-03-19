using Image.Api.Services;
using Image.Api.Services.Contracts;

namespace Image.Api.Modules
{
    public static class ServiceModule
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IBlobService, BlobService>();

            return services;
        }
    }
}
