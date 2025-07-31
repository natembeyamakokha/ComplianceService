using Compliance.Shared;
using Compliance.Domain.Enum;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Compliance.Domain.Form;

public class OnboardingJourneyForm
{
    public int Age { get; set; }
    public string Channel { get; set; }
    public string CountryCode { get; set; }
    public string CustomerName { get; set; }
    public Collection<Phone> PhoneNumbers { get; set; }
    [JsonIgnore]
    public CountryCode CountryCodeEnum
    {
        get
        {
            return System.Enum.Parse<CountryCode>(CountryCode, ignoreCase: true);
        }
    }

    public void Validate()
    {
        var validator = new OnboardingJourneyFormValidator();
        var result = validator.Validate(this);
        if (!result.IsValid) throw new SimSwapException(result.Errors);
    }
}
