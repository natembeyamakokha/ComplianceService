using Omni.Features;

namespace Compliance.Infrastructure.Providers.Rwanda.Mtn
{
    public class AirtelRwandaSimSwapRequest : IFeatureRequest<AirtelRwandaSimSwapResponse>
    {
        public string msisdn { get; set; } = string.Empty;
    }
}