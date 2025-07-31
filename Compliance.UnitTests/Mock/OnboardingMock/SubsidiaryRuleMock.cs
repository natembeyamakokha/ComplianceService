using Compliance.Domain.Enum;
using System.Threading.Tasks;
using Compliance.Domain.Mappers;
using Compliance.Infrastructure.Providers.Onboarding;
using Compliance.Infrastructure.Providers.Onboarding.BaseMiddlewares;


namespace Compliance.UnitTests.Mock.OnboardingMock
{
    public class SubsidiaryRuleMock : ISubsidiaryRule
    {
        public async Task<SubsidiaryRulesModel> GetSubsidiaryRuleAsync(string journeyName, CountryCode countryCode, string ruleName)
        {
            if (ruleName == nameof(AllowedSubsidiary))
            {
                if (countryCode == CountryCode.KE)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 2,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 1,
                        SubsidiaryCode = "KE",
                        SubsidiaryName = "Kenya",
                        TerminateOnFailure = true,
                        ConfigValue = "{\"isAllowed\":true}",
                        RuleName = "AllowedSubsidiary",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.UG)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 2,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 2,
                        SubsidiaryCode = "UG",
                        SubsidiaryName = "Uganda",
                        TerminateOnFailure = true,
                        ConfigValue = "{\"isAllowed\":false}",
                        RuleName = "AllowedSubsidiary",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.TZ)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 2,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 3,
                        SubsidiaryCode = "TZ",
                        SubsidiaryName = "Tanzania",
                        TerminateOnFailure = true,
                        ConfigValue = "{\"isAllowed\":true}",
                        RuleName = "AllowedSubsidiary",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.RW)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 2,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 4,
                        SubsidiaryCode = "RW",
                        SubsidiaryName = "Rwanda",
                        TerminateOnFailure = true,
                        ConfigValue = "{\"isAllowed\":true}",
                        RuleName = "AllowedSubsidiary",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.SS)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 2,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 5,
                        SubsidiaryCode = "SS",
                        SubsidiaryName = "SouthSudan",
                        TerminateOnFailure = true,
                        ConfigValue = "{\"isAllowed\":false}",
                        RuleName = "AllowedSubsidiary",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.DRC)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 2,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 6,
                        SubsidiaryCode = "DRC",
                        SubsidiaryName = "Democratic Republic Of Congo",
                        TerminateOnFailure = true,
                        ConfigValue = "{\"isAllowed\":true}",
                        RuleName = "AllowedSubsidiary",
                        ChannelName = "WEB"
                    };
            }

            if (ruleName == BaseWorldCheckRuleValidator.WORLD_CHECK)
            {
                if (countryCode == CountryCode.KE)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 2,
                        IsApplicable = false,
                        ChannelId = 1,
                        SubsidiaryId = 1,
                        SubsidiaryCode = "KE",
                        SubsidiaryName = "Kenya",
                        TerminateOnFailure = true,
                        ConfigValue = "",
                        RuleName = "WorldCheck",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.UG)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 2,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 2,
                        SubsidiaryCode = "UG",
                        SubsidiaryName = "Uganda",
                        TerminateOnFailure = true,
                        ConfigValue = "",
                        RuleName = "WorldCheck",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.TZ)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 2,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 3,
                        SubsidiaryCode = "TZ",
                        SubsidiaryName = "Tanzania",
                        TerminateOnFailure = true,
                        ConfigValue = "",
                        RuleName = "WorldCheck",
                        ChannelName = "WEB"
                    };
            }

            if (ruleName == nameof(BlockOnRegistration))
            {
                if (countryCode == CountryCode.KE)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 1,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 1,
                        SubsidiaryCode = "KE",
                        SubsidiaryName = "Kenya",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"isBlocked\":true}",
                        RuleName = "BlockOnRegistration",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.UG)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 1,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 2,
                        SubsidiaryCode = "UG",
                        SubsidiaryName = "Uganda",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"isBlocked\":false}",
                        RuleName = "BlockOnRegistration",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.TZ)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 1,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 3,
                        SubsidiaryCode = "TZ",
                        SubsidiaryName = "Tanzania",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"isBlocked\":false}",
                        RuleName = "BlockOnRegistration",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.RW)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 1,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 4,
                        SubsidiaryCode = "RW",
                        SubsidiaryName = "Rwanda",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"isBlocked\":false}",
                        RuleName = "BlockOnRegistration",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.SS)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 1,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 5,
                        SubsidiaryCode = "SS",
                        SubsidiaryName = "SouthSudan",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"isBlocked\":false}",
                        RuleName = "BlockOnRegistration",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.DRC)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 1,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 6,
                        SubsidiaryCode = "DRC",
                        SubsidiaryName = "Democratic Republic Of Congo",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"isBlocked\":false}",
                        RuleName = "BlockOnRegistration",
                        ChannelName = "WEB"
                    };
            }

            if (ruleName == nameof(CoolOff))
            {
                if (countryCode == CountryCode.KE)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 3,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 1,
                        SubsidiaryCode = "KE",
                        SubsidiaryName = "Kenya",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"customerLevel\":0}",
                        RuleName = "CoolOff",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.UG)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 3,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 2,
                        SubsidiaryCode = "UG",
                        SubsidiaryName = "Uganda",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"customerLevel\":1}",
                        RuleName = "CoolOff",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.TZ)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 3,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 3,
                        SubsidiaryCode = "TZ",
                        SubsidiaryName = "Tanzania",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"customerLevel\":1}",
                        RuleName = "CoolOff",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.RW)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 3,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 4,
                        SubsidiaryCode = "RW",
                        SubsidiaryName = "Rwanda",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"customerLevel\":1}",
                        RuleName = "CoolOff",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.SS)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 3,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 5,
                        SubsidiaryCode = "SS",
                        SubsidiaryName = "SouthSudan",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"customerLevel\":1}",
                        RuleName = "CoolOff",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.DRC)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 3,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 6,
                        SubsidiaryCode = "DRC",
                        SubsidiaryName = "Democratic Republic Of Congo",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"customerLevel\":1}",
                        RuleName = "CoolOff",
                        ChannelName = "WEB"
                    };
            }

            if (ruleName == nameof(MaximumAge))
            {
                if (countryCode == CountryCode.KE)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 4,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 1,
                        SubsidiaryCode = "KE",
                        SubsidiaryName = "Kenya",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"maxAge\":65}",
                        RuleName = "MaximumAge",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.UG)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 4,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 2,
                        SubsidiaryCode = "UG",
                        SubsidiaryName = "Uganda",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"maxAge\":65}",
                        RuleName = "MaximumAge",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.TZ)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 4,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 3,
                        SubsidiaryCode = "TZ",
                        SubsidiaryName = "Tanzania",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"maxAge\":65}",
                        RuleName = "MaximumAge",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.RW)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 4,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 4,
                        SubsidiaryCode = "RW",
                        SubsidiaryName = "Rwanda",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"maxAge\":65}",
                        RuleName = "MaximumAge",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.SS)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 4,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 5,
                        SubsidiaryCode = "SS",
                        SubsidiaryName = "SouthSudan",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"maxAge\":65}",
                        RuleName = "MaximumAge",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.DRC)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 4,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 6,
                        SubsidiaryCode = "DRC",
                        SubsidiaryName = "Democratic Republic Of Congo",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"maxAge\":65}",
                        RuleName = "MaximumAge",
                        ChannelName = "WEB"
                    };
            }

            if (ruleName == nameof(Simswap))
            {
                if (countryCode == CountryCode.KE)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 5,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 1,
                        SubsidiaryCode = "KE",
                        SubsidiaryName = "Kenya",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"allowedNumberOfDays\":180}",
                        RuleName = "Simswap",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.UG)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 5,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 2,
                        SubsidiaryCode = "UG",
                        SubsidiaryName = "Uganda",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"allowedNumberOfDays\":180}",
                        RuleName = "Simswap",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.TZ)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 5,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 3,
                        SubsidiaryCode = "TZ",
                        SubsidiaryName = "Tanzania",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"allowedNumberOfDays\":180}",
                        RuleName = "Simswap",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.RW)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 5,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 4,
                        SubsidiaryCode = "RW",
                        SubsidiaryName = "Rwanda",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"allowedNumberOfDays\":180}",
                        RuleName = "Simswap",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.SS)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 5,
                        IsApplicable = false,
                        ChannelId = 1,
                        SubsidiaryId = 5,
                        SubsidiaryCode = "SS",
                        SubsidiaryName = "SouthSudan",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"allowedNumberOfDays\":180}",
                        RuleName = "Simswap",
                        ChannelName = "WEB"
                    };
                if (countryCode == CountryCode.DRC)
                    return new()
                    {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 5,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 6,
                        SubsidiaryCode = "DRC",
                        SubsidiaryName = "Democratic Republic Of Congo",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"allowedNumberOfDays\":180}",
                        RuleName = "Simswap",
                        ChannelName = "WEB"
                    };
            }

            return new();
        }
    }
}