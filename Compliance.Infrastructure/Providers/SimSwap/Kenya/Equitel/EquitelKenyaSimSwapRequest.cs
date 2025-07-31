using Omni.Features;

namespace Compliance.Infrastructure.Providers.Kenya.Equitel
{
    public class EquitelKenyaSimSwapRequest : IFeatureRequest<EquitelKenyaSimSwapResponse>
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public int AllowedNumberOfDays { get; set; }
    }
}