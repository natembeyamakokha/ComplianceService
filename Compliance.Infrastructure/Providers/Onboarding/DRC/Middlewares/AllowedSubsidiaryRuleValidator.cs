using Autofac;
using Omni.Features;
using Omni.Middlewares;
using Compliance.Infrastructure.Providers.Onboarding.BaseMiddlewares;
using Compliance.Domain.Response;

namespace Compliance.Infrastructure.Providers.Onboarding.DRC.Middlewares;

#region Rule Validator
/*
 * No	Rule Description Terminate on failure
 * 1.	Subsidiary Allowed Checks whether a customer in that subsidiary is allowed to take this journey true
 * 2.	Block on registration	Checks whether a customer profile status for that subsidiary should be in a blocked state on registration.	false
 * 3.	Age Validation	checks whether to block a customer profile status based on the maximum accepted age for that subsidiary	false.
 * 4.	Cool Off Validation	put a customer on cool off or not based on configuration false.
 * 5.	IMSI (SimSwap) check	check if a customers phone number has been swapped in the past nth days	false
 */
#endregion

[DependencyFor(typeof(BlockOnRegistrationRuleValidator))]
public class AllowedSubsidiaryRuleValidator : FeatureMiddleware<DRCOnboardingJourneyRequest, OnboardingJourneyResponse>
{
    public string RuleName => nameof(AllowedSubsidiary);

    protected override async Task<IFeatureResult> Execute(FeatureHandle next, IFeatureContext context, DRCOnboardingJourneyRequest request, CancellationToken cancellationToken)
    {
        using var scope = ServiceCompositionRoot.BeginLifetimeScope();
        var baseMiddlewareValidators = scope.Resolve<IEnumerable<IBaseMiddlewareValidator>>();
        var allowedSubsidiaryValidator = baseMiddlewareValidators.FirstOrDefault(x => x.RuleName == RuleName);

        if (allowedSubsidiaryValidator == null)
            throw new InvalidOperationException($"Validator with rule name '{RuleName}' not found.");

        return await allowedSubsidiaryValidator?.ExecuteAsync(next, context, request, cancellationToken);
    }
}

