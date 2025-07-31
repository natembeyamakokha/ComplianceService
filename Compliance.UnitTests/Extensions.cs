using Autofac;
using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Omni.Repository.EFCore;
using Omni.Domain.Repositories;
using Omni.Repository.EFCore.Repositories;
using Omni.Domain.Repositories.Pagination;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;

namespace Compliance.UnitTests;

public static class Extensions
{
    public static IServiceCollection AddCustomUnitOfWork<TDbContext>(this IServiceCollection services, params Assembly[] assemblies)
       where TDbContext : OmniDbContextBase
    {
        services
            .AddCustomRepositories(assemblies)
            .AddScoped<IUnitOfWork, UnitOfWork<TDbContext>>();

        return services;
    }

    public static IServiceCollection AddCustomRepositories(this IServiceCollection services, params Assembly[] assemblies)
    {
        if (assemblies.Length == 0)
        {
            assemblies = AppDomain.CurrentDomain.GetAssemblies();
        }

        Type[] repositoryInterfaces = new Type[]
        {
            typeof(IRepository<,>),
            typeof(IReadOnlyRepository<,>),
            typeof(IWriteOnlyRepository<,>),
            typeof(IPaginatedRepository<,,>)
        };

        var interfaces = assemblies
                    .SelectMany(x => x.GetTypes())
                    .Where(x =>
                        x.IsInterface &&
                        x.GetInterfaces()
                            .Any(x => x.IsGenericType && repositoryInterfaces.Any(t => t == x.GetGenericTypeDefinition())));

        foreach (var @interface in interfaces)
        {
            var implementation = assemblies
                                    .SelectMany(x => x.GetTypes())
                                    .Where(x => !x.IsAbstract && !x.IsInterface)
                                    .FirstOrDefault(x => x.IsAssignableTo(@interface));

            if (implementation is null)
            {
                throw new InvalidOperationException($"No implementation found for {@interface.Name} interface");
            }

            services.TryAddScoped(@interface, implementation);
        }

        return services;
    }

    public static IServiceCollection AddTestInfrastructure(this IServiceCollection services)
    {
        
        return services;
    }
}
