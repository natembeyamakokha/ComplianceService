using Compliance.Domain.Enum;

namespace Compliance.Application.Contracts;

public interface ITelcoResolver
{
    bool TryResolve(CountryCode countryCode, string phoneNumber, out Telco telco);
}
