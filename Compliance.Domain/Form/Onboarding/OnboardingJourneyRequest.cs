using Compliance.Domain.Enum;
using Compliance.Domain.Response;
using System.Collections.ObjectModel;

namespace Compliance.Domain.Form;

public class OnboardingJourneyRequest : IOnboardingJourneyRequest<OnboardingJourneyResponse>
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
    public OnboardingJourneyResponse Result { get; set; } = new();

    public static OnboardingJourneyRequest Create(int age, string channel, CountryCode countryCode, Collection<Phone> phones, string customerName = "")
    {
        return new OnboardingJourneyRequest
        {
            Age = age,
            Channel = channel,
            CountryCode = countryCode,
            PhoneNumbers = phones,
            CustomerName = customerName
        };
    }
}