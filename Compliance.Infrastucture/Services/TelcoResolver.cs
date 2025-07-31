using Compliance.Domain.Enum;
using Compliance.Application.Settings;
using Compliance.Application.Contracts;

namespace Compliance.Infrastructure.Services;

public class TelcoResolver : ITelcoResolver
{
    private readonly TelcosPrefixAppSetting _telcosAppSetting;

    public TelcoResolver(TelcosPrefixAppSetting telcosAppSetting)
    {
        _telcosAppSetting = telcosAppSetting;
    }

    public bool TryResolve(CountryCode countryCode, string phoneNumber, out Telco telco)
    {
        telco = default;
        var telcoConfig = _telcosAppSetting
           .Telcos
           .FirstOrDefault(x =>
           x.IsMatch(countryCode, phoneNumber));

        if (telcoConfig == null)
            return false;

        telco = Enum.Parse<Telco>(telcoConfig.Name, ignoreCase: true);
        return true;
    }
}
