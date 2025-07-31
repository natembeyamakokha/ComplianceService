using Autofac;
using Omni.Features;
using Omni.Middlewares;
using Compliance.Domain.Response;
using Compliance.Infrastructure.Providers.Onboarding.BaseMiddlewares;

namespace Compliance.Infrastructure.Providers.Onboarding.SouthSudan.Middlewares;

public class SimswapRuleValidator : FeatureMiddleware<SouthSudanOnboardingJourneyRequest, OnboardingJourneyResponse>
{
    public string RuleName => nameof(Simswap);

    protected override async Task<IFeatureResult> Execute(FeatureHandle next, IFeatureContext context, SouthSudanOnboardingJourneyRequest request, CancellationToken cancellationToken)
    {
        using var scope = ServiceCompositionRoot.BeginLifetimeScope();
        var baseMiddlewareValidators = scope.Resolve<IEnumerable<IBaseMiddlewareValidator>>();
        var simSwapValidator = baseMiddlewareValidators.FirstOrDefault(x => x.RuleName == RuleName);

        if (simSwapValidator == null)
            throw new InvalidOperationException($"Validator with rule name '{RuleName}' not found.");

        return await simSwapValidator.ExecuteAsync(next, context, request, cancellationToken, dependentRules: nameof(BlockOnRegistration));
    }
}

