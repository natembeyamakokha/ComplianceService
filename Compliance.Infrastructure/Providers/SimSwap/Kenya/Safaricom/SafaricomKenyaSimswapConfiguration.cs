using Omni.Factory;
using Compliance.Domain.Enum;
using Compliance.Domain.Form;

namespace Compliance.Infrastructure.Providers.Kenya.Safaricom
{
    public class SafaricomKenyaSimSwapConfiguration : ConfigMap<SimSwapRequest, SafaricomSimSwapRequest>
    {
        protected override SafaricomSimSwapRequest OnMap(SimSwapRequest source)
        {
            return new SafaricomSimSwapRequest { CustomerNumber = source.PhoneNumber };
        }

        protected override BaseSelector OnSelect()
        {
            return new SimSwapSelector(CountryCode.KE, Telco.Safaricom);
        }
    }
}