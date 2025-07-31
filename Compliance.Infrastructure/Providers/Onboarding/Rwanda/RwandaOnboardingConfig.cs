using Omni.Factory;
using Compliance.Domain.Enum;
using Compliance.Domain.Form;

namespace Compliance.Infrastructure.Providers.Onboarding.Rwanda
{
    public class RwandaOnboardingConfig : ConfigMap<OnboardingJourneyRequest, RwandaOnboardingJourneyRequest>
    {
        protected override BaseSelector OnSelect() => new OnboardingSelector(CountryCode.RW);

        protected override RwandaOnboardingJourneyRequest OnMap(OnboardingJourneyRequest source)
        {
            return new RwandaOnboardingJourneyRequest()
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
