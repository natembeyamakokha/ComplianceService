using Autofac;
using MediatR;
using System;
using Omni.Modules;
using Omni.Factory;
using Omni.Storages;
using Omni.Features;
using Omni.Accessors;
using System.Threading;
using Omni.Middlewares;
using System.Reflection;
using System.Threading.Tasks;
using Compliance.Application;
using Compliance.Infrastructure;
using Compliance.UnitTests.Mock;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.TestHost;
using Compliance.Application.Settings;
using Compliance.Application.Contracts;
using Compliance.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Compliance.Domain.Entity.Interfaces;
using Compliance.UnitTests.Mock.OnboardingMock;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Compliance.Infrastructure.Providers.Onboarding;
using Compliance.Infrastructure.ApiClient;
using Compliance.UnitTests.Mock.Services;
using Compliance.Infrastructure.Domain;
using Compliance.Infrastructure.Proxy;

namespace Compliance.UnitTests
{
    internal sealed class ComplianceFixture : IDisposable
    {
        private readonly IHost _host;

        private IConfiguration? Configuration = default!;
        private Assembly ThisAssembly => typeof(ComplianceFixture).Assembly;

        internal IMapper Mapper { get; }
        internal IDataStore Storage { get; }
        internal IFeaturesModule TestModule { get; }
        internal ILogger<ComplianceFixture> Logger { get; }
        internal ITelcoResolver TelcoResolver { get; }
        internal ISimSwapService SimSwapService { get; }
        internal IStorageAccessor StorageAccessor { get; }
        internal SimSwapRequestFactory RequestFactory { get; }
        internal IEnumerable<IFeatureMiddleware> Middlewares { get; }
        internal TelcosPrefixAppSetting TelcosPrefixAppSetting { get; }
        internal IEnumerable<IFeatureProvider> FeatureProviders { get; }
        internal IOnboardingModule OnboardingModule { get; }
        internal OnboardingJourneyMapperFactory OnboardingMapperFactory { get; }
        internal ISubsidiaryRulesRepository SubsidiaryRulesRepository { get; }
        internal ISimSwapCheckTaskRepository SimSwapCheckTaskRepository { get; }
        internal ISubsidiaryRule SubsidiaryRule { get; set; }
        internal IMediator Mediator { get; set; }
        internal IBackgroundSimSwapCheckService BackgroundSimSwapCheckService { get; }

        public ComplianceFixture()
        {
            var configuration = new Dictionary<string, string>
        {
            {"Jobs:Start","false" },
            {"ConnectionStrings:DefaultConnection","Server=localhost;Database=compliance;User Id=devdb;Password=root;TrustServerCertificate=True;" },
        };

            _host = Host.CreateDefaultBuilder()
                   .ConfigureAppConfiguration(config =>
                   {
                       config.AddJsonFile("appsettings.json");
                       config.AddInMemoryCollection(configuration);
                       Configuration = config.Build();

                   })
                   .ConfigureServices((context, services) =>
                   {
                       var connectionString = configuration["ConnectionStrings:DefaultConnection"];

                       services.AddDistributedMemoryCache();
                       services.AddApplication(Configuration!);
                       services.AddSingleton(typeof(IAPIWrapper<,>), typeof(APIWrapperMock<,>));
                       services.AddInfrastructure(connectionString, Configuration);
                       services.AddScoped<ISubsidiaryRulesRepository, SubsidiaryRuleRepositoryMock>();
                       services.AddScoped<ISubsidiaryRule, SubsidiaryRuleMock>();
                       services.AddScoped(c => new WorldCheckAPIMock().MockedWorldCheckAPI.Object);

                       services.AddCustomUnitOfWork<UtilityServiceContext>(typeof(IOnboardingModule).Assembly);

                       //TODO: Remove this once the actual implementation is fixed
                       services.AddScoped<ISimSwapService, SimSwapMockService>();

                       var builder = new ContainerBuilder();
                       builder.Populate(services);
                       ServiceCompositionRoot.Set(builder.Build());
                   })
                   .ConfigureWebHostDefaults(configure =>
                   {
                       configure.UseTestServer()
                       .Configure((context, app) =>
                       {
                           //middle where
                       });
                   }).Build();

            Mapper = _host.Services.GetRequiredService<IMapper>();
            Storage = _host.Services.GetRequiredService<IDataStore>();
            Middlewares = _host.Services.GetServices<IFeatureMiddleware>();
            TestModule = _host.Services.GetRequiredService<IFeaturesModule>();
            FeatureProviders = _host.Services.GetServices<IFeatureProvider>();
            Logger = _host.Services.GetRequiredService<ILogger<ComplianceFixture>>();
            TelcoResolver = _host.Services.GetRequiredService<ITelcoResolver>();
            StorageAccessor = _host.Services.GetRequiredService<IStorageAccessor>();
            RequestFactory = _host.Services.GetRequiredService<SimSwapRequestFactory>();
            SimSwapService = _host.Services.GetRequiredService<ISimSwapService>();
            TelcosPrefixAppSetting = _host.Services.GetRequiredService<TelcosPrefixAppSetting>();
            OnboardingModule = _host.Services.GetRequiredService<IOnboardingModule>();
            OnboardingMapperFactory = _host.Services.GetRequiredService<OnboardingJourneyMapperFactory>();
            SubsidiaryRulesRepository = _host.Services.GetRequiredService<ISubsidiaryRulesRepository>();
            SimSwapCheckTaskRepository = _host.Services.GetRequiredService<ISimSwapCheckTaskRepository>();
            SubsidiaryRule = _host.Services.GetRequiredService<ISubsidiaryRule>();
            Mediator = _host.Services.GetRequiredService<IMediator>();
            BackgroundSimSwapCheckService = _host.Services.GetRequiredService<IBackgroundSimSwapCheckService>();
        }

        public static async Task<ComplianceFixture> CreateAsync()
        {
            var fixture = new ComplianceFixture();
            await fixture.SetupAsync();

            return fixture;
        }

        private async Task SetupAsync()
            => await _host.StartAsync();

        private async Task StopAsync()
            => await _host.StopAsync();

        public void Dispose()
            => _host.Dispose();

        internal static CancellationToken CreateCancellation(int seconds = 5)
        {
            return new CancellationTokenSource(TimeSpan.FromSeconds(seconds)).Token;
        }
    }
}
