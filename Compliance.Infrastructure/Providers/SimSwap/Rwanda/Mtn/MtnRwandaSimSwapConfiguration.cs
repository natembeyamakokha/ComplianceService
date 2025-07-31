using Omni.Factory;
using Compliance.Domain.Enum;
using Compliance.Domain.Form;

namespace Compliance.Infrastructure.Providers.Rwanda.Mtn
{
    public class MtnRwandaSimSwapConfiguration : ConfigMap<SimSwapRequest, MtnRwandaSimSwapRequest>
    {
        protected override MtnRwandaSimSwapRequest OnMap(SimSwapRequest source)
        {
            return new MtnRwandaSimSwapRequest { PhoneNumber = source.PhoneNumber };
        }

        protected override BaseSelector OnSelect()
        {
            return new SimSwapSelector(CountryCode.RW, Telco.Mtn);
        }
    }
}