using Omni.Features;
using Compliance.Domain.Response;
using Compliance.Domain.Enum;
using System.Collections.ObjectModel;

namespace Compliance.Domain.Form;

public interface IOnboardingJourneyRequest<TResult> : IFeatureRequest<OnboardingJourneyResponse>, IOnboardingJourneyRequest { }

public interface IOnboardingJourneyRequest : IFeatureRequest
{
    public string Key { get; }
    public int Age { get; set; }
    public string Channel { get; set; }
    public string CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string CallBackUrl { get; set; }
    public string Source { get; set; }
    public CountryCode CountryCode { get; set; }
    public Collection<Phone> PhoneNumbers { get; set; }
    public OnboardingJourneyResponse Result { get; set; }
}