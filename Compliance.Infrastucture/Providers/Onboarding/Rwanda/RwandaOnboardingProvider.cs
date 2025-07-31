using Omni.Features;
using Omni.Providers;
using Compliance.Domain.Response;

namespace Compliance.Infrastructure.Providers.Onboarding.Rwanda
{
    public sealed class RwandaOnboardingProvider : FeatureProvider<RwandaOnboardingJourneyRequest, OnboardingJourneyResponse>
    {
        protected override async Task<FeatureResult<OnboardingJourneyResponse>> ExecuteAsync(RwandaOnboardingJourneyRequest request, IFeatureContext context)
        {
            var allRulesDidntPass = request.Result.Rules.Any(x => x.Result == RuleResult.Failed.ToString());
            request.Result.AllRulesPassed = !allRulesDidntPass;
            return await Task.FromResult(FeatureResult.Succeed(request, request.Result));
        }
    }
}

