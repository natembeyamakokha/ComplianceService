using Microsoft.Extensions.DependencyInjection;

namespace Compliance.Infrastructure.ApiClient
{
    public static class Extensions
    {
        public static IServiceCollection AddApiClient(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IAPIWrapper<,>), typeof(APIWrapper<,>));
            return services;
        }
    }
}