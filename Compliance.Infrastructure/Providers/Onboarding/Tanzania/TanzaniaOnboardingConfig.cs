using Omni.Factory;
using Compliance.Domain.Enum;
using Compliance.Domain.Form;

namespace Compliance.Infrastructure.Providers.Onboarding.Tanzania
{
    public class TanzaniaOnboardingConfig : ConfigMap<OnboardingJourneyRequest, TanzaniaOnboardingJourneyRequest>
    {
        protected override BaseSelector OnSelect() => new OnboardingSelector(CountryCode.TZ);

        protected override TanzaniaOnboardingJourneyRequest OnMap(OnboardingJourneyRequest source)
        {
            return new TanzaniaOnboardingJourneyRequest()
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
