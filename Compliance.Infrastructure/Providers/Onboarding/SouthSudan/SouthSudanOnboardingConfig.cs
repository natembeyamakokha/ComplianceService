using Omni.Factory;
using Compliance.Domain.Enum;
using Compliance.Domain.Form;

namespace Compliance.Infrastructure.Providers.Onboarding.SouthSudan
{
    public class SouthSudanOnboardingConfig : ConfigMap<OnboardingJourneyRequest, SouthSudanOnboardingJourneyRequest>
    {
        protected override BaseSelector OnSelect() => new OnboardingSelector(CountryCode.SS);

        protected override SouthSudanOnboardingJourneyRequest OnMap(OnboardingJourneyRequest source)
        {
            return new SouthSudanOnboardingJourneyRequest()
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
