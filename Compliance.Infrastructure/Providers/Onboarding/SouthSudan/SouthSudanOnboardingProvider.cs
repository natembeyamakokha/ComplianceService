using Omni.Features;
using Omni.Providers;
using Compliance.Domain.Response;

namespace Compliance.Infrastructure.Providers.Onboarding.SouthSudan
{
    public sealed class SouthSudanOnboardingProvider : FeatureProvider<SouthSudanOnboardingJourneyRequest, OnboardingJourneyResponse>
    {
        protected override async Task<FeatureResult<OnboardingJourneyResponse>> ExecuteAsync(SouthSudanOnboardingJourneyRequest request, IFeatureContext context)
        {
            var allRulesDidntPass = request.Result.Rules.Any(x => x.Result == RuleResult.Failed.ToString());
            request.Result.AllRulesPassed = !allRulesDidntPass;
            return await Task.FromResult(FeatureResult.Succeed(request, request.Result));
        }
    }
}

