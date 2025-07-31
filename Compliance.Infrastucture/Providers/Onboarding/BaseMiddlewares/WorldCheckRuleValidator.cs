using Omni.Features;
using Omni.Middlewares;
using Compliance.Domain.Form;
using Compliance.Domain.Response;
using MediatR;
using Compliance.Application.UseCases.Compliance.VerifyFraudStatus;

namespace Compliance.Infrastructure.Providers.Onboarding.BaseMiddlewares
{
    public class BaseWorldCheckRuleValidator(ISubsidiaryRule subsidiaryRule, IMediator mediator) : IBaseMiddlewareValidator
    {
        public string RuleName => "WorldCheck";
        public const string WORLD_CHECK = "WorldCheck";
        private readonly ISubsidiaryRule _subsidiaryRule = subsidiaryRule;
        private readonly IMediator _mediator = mediator;

        public async Task<IFeatureResult> ExecuteAsync(FeatureHandle next, IFeatureContext context, IFeatureRequest featureRequest, CancellationToken cancellationToken, params string[] dependentRules)
        {

            var request = featureRequest as IOnboardingJourneyRequest;

            var worldCheckRules = await _subsidiaryRule.GetSubsidiaryRuleAsync(Constants.Onboarding, request.CountryCode, RuleName);

            var isApplicable = MiddlewareHelper.CheckIfRuleIsApplicableAsync(request, RuleName, worldCheckRules, dependentRules);
            if (!isApplicable)
                return await next(context, request, cancellationToken).ConfigureAwait(false);

            var result = await _mediator.Send(
                new VerifyFraudStatusCommand(request.CustomerName, "INDIVIDUAL"),
                cancellationToken);

            if (!result.ResponseObject.IsCompliant)
            {
                request.Result.PopulateRuleResult(new Rule
                {
                    Name = RuleName,
                    Code = RuleName,
                    Message = nameof(RuleResult.Failed),
                    Result = nameof(RuleResult.Failed),
                    TerminateOnFailure = worldCheckRules.TerminateOnFailure
                });
            }
            else
            {
                request.Result.PopulateRuleResult(new Rule
                {
                    Name = RuleName,
                    Code = RuleName,
                    Message = nameof(RuleResult.Succeed),
                    Result = nameof(RuleResult.Succeed)
                });
            }

            return await next(context, request, cancellationToken).ConfigureAwait(false);
        }
    }
}