using Compliance.Domain.Enum;
using Compliance.Domain.Mappers;
using Compliance.Domain.Entity.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Compliance.UnitTests.Mock
{

    public static class KEMockSubsidiaryRule
    {
        public static List<SubsidiaryRulesModel> SubsidiaryBlockOnRegistrationKE = new List<SubsidiaryRulesModel>()
        {
            new() {
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
            },
            new() {
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
            },
            new() {
                JourneyId = 1,
                JourneyName = "Onboarding",
                RuleId = 3,
                IsApplicable = true,
                ChannelId = 1,
                SubsidiaryId = 1,
                SubsidiaryCode = "KE",
                SubsidiaryName = "Kenya",
                TerminateOnFailure = false,
                ConfigValue = "{\"customerLevel\":1}",
                RuleName = "CoolOff",
                ChannelName = "WEB"
            },
            new() {
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
            },
            new() {
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
            }
        };
    }

    public class SubsidiaryRuleRepositoryMock : ISubsidiaryRulesRepository
    {
        public List<SubsidiaryRulesModel> SubsidiaryRuleNotAllowedKE = new List<SubsidiaryRulesModel>()
        {
            new() {
                JourneyId = 1,
                JourneyName = "Onboarding",
                RuleId = 1,
                IsApplicable = true,
                ChannelId = 1,
                SubsidiaryId = 1,
                SubsidiaryCode = "KE",
                SubsidiaryName = "Kenya",
                TerminateOnFailure = false,
                ConfigValue = "{\"isBlocked\":false}",
                RuleName = "BlockOnRegistration",
                ChannelName = "WEB"
            },
            new() {
                JourneyId = 1,
                JourneyName = "Onboarding",
                RuleId = 2,
                IsApplicable = true,
                ChannelId = 1,
                SubsidiaryId = 1,
                SubsidiaryCode = "KE",
                SubsidiaryName = "Kenya",
                TerminateOnFailure = true,
                ConfigValue = "{\"isAllowed\":false}",
                RuleName = "AllowedSubsidiary",
                ChannelName = "WEB"
            },
            new() {
                JourneyId = 1,
                JourneyName = "Onboarding",
                RuleId = 3,
                IsApplicable = true,
                ChannelId = 1,
                SubsidiaryId = 1,
                SubsidiaryCode = "KE",
                SubsidiaryName = "Kenya",
                TerminateOnFailure = false,
                ConfigValue = "{\"customerLevel\":1}",
                RuleName = "CoolOff",
                ChannelName = "WEB"
            },
            new() {
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
            },
            new() {
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
            }
        };

        public List<SubsidiaryRulesModel> SubsidiaryBlockOnRegistrationKE = new List<SubsidiaryRulesModel>()
        {
            new() {
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
            },
            new() {
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
            },
            new() {
                JourneyId = 1,
                JourneyName = "Onboarding",
                RuleId = 3,
                IsApplicable = true,
                ChannelId = 1,
                SubsidiaryId = 1,
                SubsidiaryCode = "KE",
                SubsidiaryName = "Kenya",
                TerminateOnFailure = false,
                ConfigValue = "{\"customerLevel\":1}",
                RuleName = "CoolOff",
                ChannelName = "WEB"
            },
            new() {
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
            },
            new() {
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
            }
        };

        public async Task<List<SubsidiaryRulesModel>> GetSubsidiaryRulesAsync(string journeyName, CountryCode countryCode)
        {

            if (countryCode == CountryCode.KE)
            {
                return await Task.FromResult(new List<SubsidiaryRulesModel>{
                    new() {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 1,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 1,
                        SubsidiaryCode = "KE",
                        SubsidiaryName = "Kenya",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"isBlocked\":false}",
                        RuleName = "BlockOnRegistration",
                        ChannelName = "WEB"
                    },
                    new() {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 2,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 1,
                        SubsidiaryCode = "KE",
                        SubsidiaryName = "Kenya",
                        TerminateOnFailure = true,
                        ConfigValue = "{\"isAllowed\":false}",
                        RuleName = "AllowedSubsidiary",
                        ChannelName = "WEB"
                    },
                    new() {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 3,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 1,
                        SubsidiaryCode = "KE",
                        SubsidiaryName = "Kenya",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"customerLevel\":1}",
                        RuleName = "CoolOff",
                        ChannelName = "WEB"
                    },
                    new() {
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
                    },
                    new() {
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
                    }
                });
            }

            if (countryCode == CountryCode.UG)
            {
                return await Task.FromResult(new List<SubsidiaryRulesModel>{
                    new() {
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
                    },
                    new() {
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
                    },
                    new() {
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
                    },
                    new() {
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
                    },
                    new() {
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
                    }
                });
            }

            if (countryCode == CountryCode.TZ)
            {
                return await Task.FromResult(new List<SubsidiaryRulesModel>{
                    new() {
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
                    },
                    new() {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 2,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 3,
                        SubsidiaryCode = "TZ",
                        SubsidiaryName = "Tanzania",
                        TerminateOnFailure = true,
                        ConfigValue = "{\"isAllowed\":false}",
                        RuleName = "AllowedSubsidiary",
                        ChannelName = "WEB"
                    },
                    new() {
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
                    },
                    new() {
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
                    },
                    new() {
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
                    }
                });
            }

            if (countryCode == CountryCode.RW)
            {
                return await Task.FromResult(new List<SubsidiaryRulesModel>{
                    new() {
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
                    },
                    new() {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 2,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 4,
                        SubsidiaryCode = "RW",
                        SubsidiaryName = "Rwanda",
                        TerminateOnFailure = true,
                        ConfigValue = "{\"isAllowed\":false}",
                        RuleName = "AllowedSubsidiary",
                        ChannelName = "WEB"
                    },
                    new() {
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
                    },
                    new() {
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
                    },
                    new() {
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
                    }
                });
            }

            if (countryCode == CountryCode.SS)
            {
                return await Task.FromResult(new List<SubsidiaryRulesModel>{
                    new() {
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
                    },
                    new() {
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
                    },
                    new() {
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
                    },
                    new() {
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
                    },
                    new() {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 5,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 5,
                        SubsidiaryCode = "SS",
                        SubsidiaryName = "SouthSudan",
                        TerminateOnFailure = false,
                        ConfigValue = "{\"allowedNumberOfDays\":180}",
                        RuleName = "Simswap",
                        ChannelName = "WEB"
                    }
                });
            }

            if (countryCode == CountryCode.DRC)
            {
                return await Task.FromResult(new List<SubsidiaryRulesModel>{
                    new() {
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
                    },
                    new() {
                        JourneyId = 1,
                        JourneyName = "Onboarding",
                        RuleId = 2,
                        IsApplicable = true,
                        ChannelId = 1,
                        SubsidiaryId = 6,
                        SubsidiaryCode = "DRC",
                        SubsidiaryName = "Democratic Republic Of Congo",
                        TerminateOnFailure = true,
                        ConfigValue = "{\"isAllowed\":false}",
                        RuleName = "AllowedSubsidiary",
                        ChannelName = "WEB"
                    },
                    new() {
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
                    },
                    new() {
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
                    },
                    new() {
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
                    }
                });
            }

            return await Task.FromResult(new List<SubsidiaryRulesModel>());
        }
    }
}