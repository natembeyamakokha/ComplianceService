using Microsoft.Extensions.DependencyInjection;

namespace Compliance.Infrastructure.Providers.Onboarding.BaseMiddlewares
{
    public static class Extensions
    {
        public static IServiceCollection AddBaseMiddlewareValidators(this IServiceCollection services)
        {
            services.AddScoped<IBaseMiddlewareValidator, BaseAllowedSubsidiaryRuleValidator>();
            services.AddScoped<IBaseMiddlewareValidator, BaseBlockOnRegistrationRuleValidator>();
            services.AddScoped<IBaseMiddlewareValidator, BaseCoolOffRuleValidator>();
            services.AddScoped<IBaseMiddlewareValidator, BaseMaximumAgeRuleValidator>();
            services.AddScoped<IBaseMiddlewareValidator, BaseSimswapRuleValidator>();
            services.AddScoped<IBaseMiddlewareValidator, BaseWorldCheckRuleValidator>();
            return services;
        }

        public static IServiceCollection AddSubsidiaryRuleSelector(this IServiceCollection services)
        {
            services.AddScoped<ISubsidiaryRule, SubsidiaryRule>();
            return services;
        }
    }
}