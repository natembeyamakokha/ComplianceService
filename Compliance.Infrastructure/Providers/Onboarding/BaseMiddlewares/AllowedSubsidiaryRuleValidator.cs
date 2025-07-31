using Omni.Features;
using Newtonsoft.Json;
using Omni.Middlewares;
using Compliance.Domain.Form;
using Compliance.Domain.Response;

namespace Compliance.Infrastructure.Providers.Onboarding.BaseMiddlewares
{

    public class BaseAllowedSubsidiaryRuleValidator : IBaseMiddlewareValidator
    {
        public string RuleName => nameof(AllowedSubsidiary);
        private readonly ISubsidiaryRule _subsidiaryRule;

        public BaseAllowedSubsidiaryRuleValidator(ISubsidiaryRule subsidiaryRule)
        {
            _subsidiaryRule = subsidiaryRule;
        }

        public async Task<IFeatureResult> ExecuteAsync(FeatureHandle next, IFeatureContext context, IFeatureRequest featureRequest, CancellationToken cancellationToken, params string[] dependentRules)
        {

            var request = featureRequest as IOnboardingJourneyRequest;

            var allowedSubsidiaryRules = await _subsidiaryRule.GetSubsidiaryRuleAsync(Constants.Onboarding, request.CountryCode, RuleName);

            var isApplicable = MiddlewareHelper.CheckIfRuleIsApplicableAsync(request, RuleName, allowedSubsidiaryRules, dependentRules);
            if (!isApplicable)
                return await next(context, request, cancellationToken).ConfigureAwait(false);

            var ruleDefinition = JsonConvert.DeserializeObject<AllowedSubsidiary>(allowedSubsidiaryRules.ConfigValue.ToString());

            if (!ruleDefinition.IsAllowed)
            {
                request.Result.PopulateRuleResult(new Rule
                {
                    Name = RuleName,
                    Code = RuleName,
                    Message = nameof(RuleResult.Failed),
                    Result = nameof(RuleResult.Failed),
                    TerminateOnFailure = allowedSubsidiaryRules.TerminateOnFailure
                });

                return await next(context, request, cancellationToken).ConfigureAwait(false);
            }

            request.Result.PopulateRuleResult(new Rule
            {
                Name = RuleName,
                Code = RuleName,
                Message = nameof(RuleResult.Succeed),
                Result = nameof(RuleResult.Succeed)
            });

            return await next(context, request, cancellationToken).ConfigureAwait(false);
        }
    }

    public class AllowedSubsidiary
    {
        public bool IsAllowed { get; set; }
    }
}