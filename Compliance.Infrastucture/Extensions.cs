using Omni;
using Microsoft.AspNetCore.Builder;
using Compliance.Infrastructure.Domain;
using Compliance.Application.Contracts;
using Microsoft.EntityFrameworkCore;
using Compliance.Infrastructure.Services;
using Compliance.Infrastructure.Providers;
using Microsoft.Extensions.DependencyInjection;
using Compliance.Infrastructure.Providers.Rwanda.Airtel;
using Compliance.Infrastructure.Providers.Kenya.Safaricom;
using Compliance.Application.Commands;
using Compliance.Domain.Enum;
using Compliance.Infrastructure.Domains;
using Compliance.Infrastructure.Providers.Onboarding;
using Compliance.Infrastructure.Providers.Onboarding.BaseMiddlewares;
using Compliance.Infrastructure.Activities;
using Compliance.Domain.Configurations;
using Microsoft.Extensions.Configuration;
using Compliance.Application.UseCases.SimSwap.ProcessNotifySimSwapResult;
using Omni.Repository.EFCore.Extensions;
using Compliance.Domain.Entity;
using OmniHttpClient.Configurations;
using OmniHttpClient.Extensions;
using Compliance.Infrastructure.Proxy;
using Compliance.Infrastructure.Services.WorldCheck;
using Compliance.Application.UseCases.Compliance.VerifyFraudStatus;
using Compliance.Infrastructure.Services.WorldCheck.IndividualScreeningRequestAndResponse;
using Compliance.Application.UseCases.Compliance.IndividualScreening;

namespace Compliance.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(
                                     this IServiceCollection services,
                                     string connectionString,
                                     IConfiguration configuration)
    {
        services.AddScoped<SafaricomProviderHelper>();
        services.AddSingleton<SimSwapRequestFactory>();
        services.AddScoped<AirtelRwandaProviderHelper>();
        services.AddSingleton<ITelcoResolver, TelcoResolver>();
        services.AddSingleton<OnboardingJourneyMapperFactory>();
        services.AddTransient<ISimSwapService, SimSwapService>();
        services.AddTransient<INotifySimSwapResultActivity, NotifySimSwapResultActivity>();
        services.AddScoped<IWorldCheckService, WorldCheckService1>();
        services.AddScoped<IVerifyFraudStatusActivity, VerifyFraudStatusActivity>();
        services.AddScoped<IIndividualScreeningActivity, IndividualScreeningActivity>();
        services.AddScoped<IIndividualWorldCheckScreeningService, IndividualWorldCheckScreeningService>();
        services.AddScoped<IBackgroundSimSwapCheckService, BackgroundSimSwapCheckService>();

        services.ConfigureOmni(builder =>
        {
            builder.OnExecutingAssemblies(typeof(SimSwapSelector).Assembly, typeof(CommandBase).Assembly, typeof(CountryCode).Assembly);
        });

        Action<AuthOptions> authOptions = options =>
            {
                options.ClientCredential = new ClientCredential
                {
                    ClientId = configuration["ClientId"],
                    ClientSecret = configuration["ClientSecret"]
                };
            };


        services.AddScoped<ApiHeaders>();
        services.AddOmniHttpClient<IWorldCheckApi>(configuration, "world-check-service");
        services.AddOmniHttpClient<IIndividualWorldCheckApi>(configuration, "world-check-service");

        services.AddUnitOfWork<UtilityServiceContext>(typeof(UtilityServiceContext).Assembly, typeof(Channels).Assembly);

        services.AddDbContext<UtilityServiceContext>(option =>
        {
            option.UseSqlServer(connectionString, b =>
             {
                 b.MigrationsAssembly("Compliance.Infrastructure");
                 b.EnableRetryOnFailure();
             });
        });

        services.AddBaseMiddlewareValidators();
        services.AddSubsidiaryRuleSelector();

        services.Configure<SimSwapCheckTaskJobSetting>(
            configuration.GetSection(nameof(SimSwapCheckTaskJobSetting)));
        SimSwapCheckTaskJobSetting simSwapCheckTaskJobSetting = configuration.GetSection(nameof(SimSwapCheckTaskJobSetting)).Get<SimSwapCheckTaskJobSetting>();
        var jobSetting = simSwapCheckTaskJobSetting;
        services.AddSingleton(jobSetting);
        //services.RegisterBackgroundSimSwapRunner(
        //    configuration.
        //    GetSection(nameof(SimSwapCheckTaskJobSetting)).
        //    Get<SimSwapCheckTaskJobSetting>());

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {

        try
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<UtilityServiceContext>();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
        }
        return app;
    }
}