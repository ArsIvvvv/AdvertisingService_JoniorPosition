using AdvertisingService.Data;
using AdvertisingService.Service;

namespace AdvertisingService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAdvertisingServices(this IServiceCollection services)
        {
            services.AddSingleton<IAdvertisingRepository, AdvertisingRepository>();
            services.AddScoped<IAdvertisingServiceJson, AdvertisingServiceJson>();

            return services;
        }
    }
}
