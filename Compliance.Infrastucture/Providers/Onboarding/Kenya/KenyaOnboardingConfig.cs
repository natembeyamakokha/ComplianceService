using Omni.Factory;
using Compliance.Domain.Enum;
using Compliance.Domain.Form;

namespace Compliance.Infrastructure.Providers.Onboarding.Kenya
{
    public class KenyaOnboardingConfig : ConfigMap<OnboardingJourneyRequest, KenyaOnboardingJourneyRequest>
    {
        protected override BaseSelector OnSelect() => new OnboardingSelector(CountryCode.KE);

        protected override KenyaOnboardingJourneyRequest OnMap(OnboardingJourneyRequest source)
        {
            return new KenyaOnboardingJourneyRequest()
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
