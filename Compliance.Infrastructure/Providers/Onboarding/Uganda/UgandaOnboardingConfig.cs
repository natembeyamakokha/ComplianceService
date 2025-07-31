using Omni.Factory;
using Compliance.Domain.Enum;
using Compliance.Domain.Form;

namespace Compliance.Infrastructure.Providers.Onboarding.Uganda
{
    public class UgandaOnboardingConfig : ConfigMap<OnboardingJourneyRequest, UgandaOnboardingJourneyRequest>
    {
        protected override BaseSelector OnSelect() => new OnboardingSelector(CountryCode.UG);

        protected override UgandaOnboardingJourneyRequest OnMap(OnboardingJourneyRequest source)
        {
            return new UgandaOnboardingJourneyRequest()
            {
                Age = source.Age,
                Channel = source.Channel,
                PhoneNumbers = source.PhoneNumbers,
                CountryCode = source.CountryCode,
                CustomerName = source.CustomerName
            };
        }
    }
}
