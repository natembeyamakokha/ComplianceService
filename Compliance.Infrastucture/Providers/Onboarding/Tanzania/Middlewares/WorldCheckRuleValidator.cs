// using Autofac;
// using Omni.Features;
// using Omni.Middlewares;
// using Compliance.Infrastructure.Providers.Onboarding.BaseMiddlewares;
// using Compliance.Domain.Response;

// namespace Compliance.Infrastructure.Providers.Onboarding.Tanzania.Middlewares;

// [DependencyFor(typeof(BlockOnRegistrationRuleValidator))]
// public class WorldCheckRuleValidator : FeatureMiddleware<TanzaniaOnboardingJourneyRequest, OnboardingJourneyResponse>
// {
//     public string RuleName => "WorldCheck";

//     protected override async Task<IFeatureResult> Execute(FeatureHandle next, IFeatureContext context, TanzaniaOnboardingJourneyRequest request, CancellationToken cancellationToken)
//     {
//         using var scope = ServiceCompositionRoot.BeginLifetimeScope();
//         var baseMiddlewareValidators = scope.Resolve<IEnumerable<IBaseMiddlewareValidator>>();
//         var coolOffValidator = baseMiddlewareValidators.FirstOrDefault(x => x.RuleName == RuleName);

//         if (coolOffValidator == null)
//             throw new InvalidOperationException($"Validator with rule name '{RuleName}' not found.");

//         return await coolOffValidator.ExecuteAsync(next, context, request, cancellationToken);
//     }
// }