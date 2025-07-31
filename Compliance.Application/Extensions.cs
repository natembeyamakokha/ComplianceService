using MediatR;
using System.Reflection;
using Compliance.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Compliance.Application
{
    public static class Extensions
    {

        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var caseScreeningSetting = new CaseScreeningSettings();
            configuration.GetRequiredSection(nameof(CaseScreeningSettings)).Bind(caseScreeningSetting);
            services.AddSingleton(caseScreeningSetting);

            var telcoPrefixSetting = new TelcosPrefixAppSetting();
            configuration.GetRequiredSection(TelcosPrefixAppSetting.KEY).Bind(telcoPrefixSetting);
            services.AddSingleton(telcoPrefixSetting);

            var resolutionMessageSettings = new ResolutionMessageSettings();
            configuration.GetRequiredSection(nameof(ResolutionMessageSettings)).Bind(resolutionMessageSettings);
            services.AddSingleton(resolutionMessageSettings);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}