using Omni.Factory;
using Compliance.Domain.Enum;

namespace Compliance.Infrastructure.Providers
{
    public record SimSwapSelector(CountryCode CountryCode, Telco Telco) : BaseSelector { }
}
