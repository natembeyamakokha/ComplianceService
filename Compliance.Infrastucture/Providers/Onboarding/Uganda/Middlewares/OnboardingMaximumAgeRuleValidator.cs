using Autofac;
using Omni.Features;
using Omni.Middlewares;
using Compliance.Domain.Response;
using Compliance.Infrastructure.Providers.Onboarding.BaseMiddlewares;

namespace Compliance.Infrastructure.Providers.Onboarding.Uganda.Middlewares;

[DependencyFor(typeof(CoolOffRuleValidator))]
public class OnboardingMaximumAgeRuleValidator : FeatureMiddleware<UgandaOnboardingJourneyRequest, OnboardingJourneyResponse>
{
    public string RuleName => nameof(MaximumAge);

    protected override async Task<IFeatureResult> Execute(FeatureHandle next, IFeatureContext context, UgandaOnboardingJourneyRequest request, CancellationToken cancellationToken)
    {
        using var scope = ServiceCompositionRoot.BeginLifetimeScope();
        var baseMiddlewareValidators = scope.Resolve<IEnumerable<IBaseMiddlewareValidator>>();
        var maxAgeValidator = baseMiddlewareValidators.FirstOrDefault(x => x.RuleName == RuleName);

        if (maxAgeValidator == null)
            throw new InvalidOperationException($"Validator with rule name '{RuleName}' not found.");

        return await maxAgeValidator.ExecuteAsync(next, context, request, cancellationToken, dependentRules: nameof(BlockOnRegistration));
    }
}

