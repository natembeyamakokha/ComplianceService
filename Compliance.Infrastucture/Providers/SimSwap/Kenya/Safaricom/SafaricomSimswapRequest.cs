using Omni.Features;

namespace Compliance.Infrastructure.Providers.Kenya.Safaricom
{
    public class SafaricomSimSwapRequest : IFeatureRequest<SafaricomKenyaSimSwapResponse>
    {
        public string CustomerNumber { get; set; } = string.Empty;
    }
}