using Omni.Features;

namespace Compliance.Infrastructure.Providers.Rwanda.Mtn
{
    public class MtnRwandaSimSwapRequest : IFeatureRequest<MtnRwandaSimSwapResponse>
    {
        public string PhoneNumber { get; set; } = string.Empty;
    }
}