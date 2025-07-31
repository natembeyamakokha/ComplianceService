using Omni.Factory;
using Compliance.Domain.Enum;
using Compliance.Domain.Form;

namespace Compliance.Infrastructure.Providers.Onboarding.DRC
{
    public class DRCOnboardingConfig : ConfigMap<OnboardingJourneyRequest, DRCOnboardingJourneyRequest>
    {
        protected override BaseSelector OnSelect() => new OnboardingSelector(CountryCode.DRC);

        protected override DRCOnboardingJourneyRequest OnMap(OnboardingJourneyRequest source)
        {
            return new DRCOnboardingJourneyRequest()
            {
                Age = source.Age,
                Channel = source.Channel,
                PhoneNumbers = source.PhoneNumbers,
                CountryCode = source.CountryCode,
                CustomerId = source.CustomerId,
                CustomerName = source.CustomerName
            };
        }
    }
}
