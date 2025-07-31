using Omni.Factory;
using Compliance.Domain.Enum;
using Compliance.Domain.Form;
using Compliance.Infrastructure.Providers.Rwanda.Airtel;

namespace Compliance.Infrastructure.Providers.Rwanda.Mtn
{
    public class AirtelRwandaSimSwapConfiguration : ConfigMap<SimSwapRequest, AirtelRwandaSimSwapRequest>
    {
        protected override AirtelRwandaSimSwapRequest OnMap(SimSwapRequest source)
        {
            return new AirtelRwandaSimSwapRequest { msisdn = source.PhoneNumber.GetLast(9) };
        }

        protected override BaseSelector OnSelect()
        {
            return new SimSwapSelector(CountryCode.RW, Telco.Airtel);
        }
    }
}