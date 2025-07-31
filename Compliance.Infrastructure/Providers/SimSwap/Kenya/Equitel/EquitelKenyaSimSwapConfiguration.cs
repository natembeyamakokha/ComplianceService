using Omni.Factory;
using Compliance.Domain.Enum;
using Compliance.Domain.Form;

namespace Compliance.Infrastructure.Providers.Kenya.Equitel
{
    public class EquitelKenyaSimSwapConfiguration : ConfigMap<SimSwapRequest, EquitelKenyaSimSwapRequest>
    {
        protected override EquitelKenyaSimSwapRequest OnMap(SimSwapRequest source)
        {
            return new EquitelKenyaSimSwapRequest { PhoneNumber = source.PhoneNumber, AllowedNumberOfDays = source.AllowedSwappedDays };
        }

        protected override BaseSelector OnSelect()
        {
            return new SimSwapSelector(CountryCode.KE, Telco.Equitel);
        }
    }
}