using Omni.Factory;
using Omni.Features;
using Compliance.Domain.Enum;
using Compliance.Domain.Form;

namespace Compliance.Infrastructure.Providers.Uganda.Mtn
{
    public class MtnUgandaSimSwapConfiguration : ConfigMap<SimSwapRequest, MtnUgandaSimSwapRequest>
    {
        protected override MtnUgandaSimSwapRequest OnMap(SimSwapRequest source)
        {
            return new MtnUgandaSimSwapRequest { PhoneNumber = source.PhoneNumber.Remove(0, 3)};
        }

        protected override BaseSelector OnSelect()
        {
            return new SimSwapSelector(CountryCode.UG, Telco.Mtn);
        }
    }
}