using Compliance.Shared;
using Compliance.Domain.Enum;
using Compliance.Domain.Response;

namespace Compliance.Application.Contracts;

public interface ISimSwapService
{
    Task<Result<SimSwapResponse>> GetLastSwappedDateAsync(string phoneNumber, CountryCode countryCode, Telco teclco, int withinTheLastDays = 180);
}
