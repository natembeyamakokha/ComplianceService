using Microsoft.Extensions.DependencyInjection;
using Compliance.Shared.DataAccess;

namespace Compliance.Shared
{
    public static class Extensions
    {
        public static IServiceCollection AddShared(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<ISqlConnectionFactory>(c =>
            {
                return new SqlConnectionFactory(connectionString);
            });
            
            return services;
        }
    }
}