using Compliance.Domain.Enum;
using Compliance.Domain.Form;
using Compliance.Domain.Mappers;
using Compliance.Domain.Response;
using System.Collections.ObjectModel;

namespace Compliance.Infrastructure.Providers.Onboarding.SouthSudan
{
    public class SouthSudanOnboardingJourneyRequest : IOnboardingJourneyRequest<OnboardingJourneyResponse>
    {
        public int Age { get; set; }
        public string Channel { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CallBackUrl { get; set; }
        public string Source { get; set; }
        public CountryCode CountryCode { get; set; }
        public Collection<Phone> PhoneNumbers { get; set; }
        public string Key => $"{Age}_{Channel}_{CountryCode}";
        public List<SubsidiaryRulesModel> SubsidiaryRules { get; set; }
        public OnboardingJourneyResponse Result { get; set; } = new();
    }
}
