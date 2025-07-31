using Omni.Features;
using Omni.Providers;

namespace Compliance.Infrastructure.Providers.Kenya.Equitel
{
    internal sealed class EquitelKenyaSimSwapProvider : FeatureProvider<EquitelKenyaSimSwapRequest, EquitelKenyaSimSwapResponse>
    {
        protected override async Task<FeatureResult<EquitelKenyaSimSwapResponse>> ExecuteAsync(EquitelKenyaSimSwapRequest request, IFeatureContext context)
        {
            await Task.CompletedTask;
            return FeatureResult.Succeed(request, new EquitelKenyaSimSwapResponse(){
                ApiReached = true,
                IsSuccessful = true,
                LastSwap = DateTime.Today.AddDays(-request.AllowedNumberOfDays)
            });
        }
    }
}