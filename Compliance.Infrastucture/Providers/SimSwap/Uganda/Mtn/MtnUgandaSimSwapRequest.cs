using Omni.Features;

namespace Compliance.Infrastructure.Providers.Uganda.Mtn
{
    public class MtnUgandaSimSwapRequest : IFeatureRequest<MtnUgandaSimSwapResponse>
    {
        public string PhoneNumber { get; set; } = string.Empty;
    }
}