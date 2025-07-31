using Compliance.Domain.Enum;
using Compliance.Domain.Mappers;

namespace Compliance.Infrastructure.Providers.Onboarding
{
    public interface ISubsidiaryRule
    {
        Task<SubsidiaryRulesModel> GetSubsidiaryRuleAsync(string journeyName, CountryCode countryCode, string ruleName);
    }

    public static class Constants
    {
        public const string Onboarding = nameof(Onboarding);
    }
}