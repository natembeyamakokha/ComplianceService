using Omni.Features;
using Newtonsoft.Json;
using Omni.Middlewares;
using Compliance.Domain.Form;
using Compliance.Domain.Response;

namespace Compliance.Infrastructure.Providers.Onboarding.BaseMiddlewares
{
    public class BaseBlockOnRegistrationRuleValidator : IBaseMiddlewareValidator
    {
        public string RuleName => nameof(BlockOnRegistration);
        private readonly ISubsidiaryRule _subsidiaryRule;

        public BaseBlockOnRegistrationRuleValidator(ISubsidiaryRule subsidiaryRule)
        {
            _subsidiaryRule = subsidiaryRule;
        }

        public async Task<IFeatureResult> ExecuteAsync(FeatureHandle next, IFeatureContext context, IFeatureRequest featureRequest, CancellationToken cancellationToken, params string[] dependentRules)
        {
            var request = featureRequest as IOnboardingJourneyRequest;

            var blockOnRegistrationRule = await _subsidiaryRule.GetSubsidiaryRuleAsync(Constants.Onboarding, request.CountryCode, RuleName);

            var isApplicable = MiddlewareHelper.CheckIfRuleIsApplicableAsync(request, RuleName, blockOnRegistrationRule, dependentRules);
            if (!isApplicable)
                return await next(context, request, cancellationToken).ConfigureAwait(false);

            var ruleDefinition = JsonConvert.DeserializeObject<BlockOnRegistration>(blockOnRegistrationRule.ConfigValue.ToString());
            if (ruleDefinition.IsBlocked)
            {
                request.Result.PopulateRuleResult(new Rule
                {
                    Name = RuleName,
                    Code = RuleName,
                    Message = nameof(RuleResult.Failed),
                    Result = nameof(RuleResult.Failed),
                    ResultValue = false,
                    TerminateOnFailure = blockOnRegistrationRule.TerminateOnFailure
                });
                return await next(context, request, cancellationToken).ConfigureAwait(false);
            }

            request.Result.PopulateRuleResult(new Rule
            {
                Name = RuleName,
                Code = RuleName,
                Message = nameof(RuleResult.Succeed),
                Result = nameof(RuleResult.Succeed),
                ResultValue = true
            });

            return await next(context, request, cancellationToken).ConfigureAwait(false);

        }
    }

    public class BlockOnRegistration
    {
        public bool IsBlocked { get; set; }
    }
}