using Autofac;
using Omni.Features;
using Omni.Middlewares;
using Compliance.Domain.Response;
using Compliance.Infrastructure.Providers.Onboarding.BaseMiddlewares;

namespace Compliance.Infrastructure.Providers.Onboarding.Kenya.Middlewares;

[DependencyFor(typeof(OnboardingMaximumAgeRuleValidator))]
public class BlockOnRegistrationRuleValidator : FeatureMiddleware<KenyaOnboardingJourneyRequest, OnboardingJourneyResponse>
{
    public string RuleName => nameof(BlockOnRegistration);

    protected override async Task<IFeatureResult> Execute(FeatureHandle next, IFeatureContext context, KenyaOnboardingJourneyRequest request, CancellationToken cancellationToken)
    {
        using var scope = ServiceCompositionRoot.BeginLifetimeScope();
        var baseMiddlewareValidators = scope.Resolve<IEnumerable<IBaseMiddlewareValidator>>();
        var blockOnRegistrationalidator = baseMiddlewareValidators.FirstOrDefault(x => x.RuleName == RuleName);

         if (blockOnRegistrationalidator == null)
            throw new InvalidOperationException($"Validator with rule name '{RuleName}' not found.");

        return await blockOnRegistrationalidator.ExecuteAsync(next, context, request, cancellationToken);
    }
}