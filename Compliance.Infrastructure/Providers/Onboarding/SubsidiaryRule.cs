using Autofac;
using Compliance.Domain.Enum;
using Compliance.Domain.Mappers;
using Compliance.Infrastructure;
using Compliance.Domain.Entity.Interfaces;

namespace Compliance.Infrastructure.Providers.Onboarding
{
    public class SubsidiaryRule : ISubsidiaryRule
    {
        public async Task<SubsidiaryRulesModel> GetSubsidiaryRuleAsync(string journeyName, CountryCode countryCode, string ruleName)
        {
            using var scope = ServiceCompositionRoot.BeginLifetimeScope();
            var repository = scope.Resolve<ISubsidiaryRulesRepository>();
            var result = await repository.GetSubsidiaryRulesAsync(journeyName, countryCode);
            return result.FirstOrDefault(x => x.RuleName == ruleName);
        }
    }
}