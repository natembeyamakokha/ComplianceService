using Compliance.Domain.Enum;
using System.Text.RegularExpressions;


namespace Compliance.Application.Settings;
public class TelcoPrefix
{
    public string Name { get; set; } = null!;
    public CountryCode CountryCode { get; set; }
    public string Pattern { get; set; } = null!;

    public bool IsMatch(CountryCode countryCode, string phoneNumber)
    {
        return new Regex(Pattern).IsMatch(phoneNumber) && CountryCode == countryCode;
    }
}

public class TelcosPrefixAppSetting
{
    public const string KEY = "TelcosPrefix";
    public TelcoPrefix[] Telcos { get; set; } = Array.Empty<TelcoPrefix>();
}
