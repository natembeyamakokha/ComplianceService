using Omni.Features;
using Newtonsoft.Json;
using Omni.Middlewares;
using Compliance.Domain.Form;
using Compliance.Domain.Response;

namespace Compliance.Infrastructure.Providers.Onboarding.BaseMiddlewares
{
    public class BaseCoolOffRuleValidator : IBaseMiddlewareValidator
    {
        public string RuleName => nameof(CoolOff);
        private readonly ISubsidiaryRule _subsidiaryRule;

        public BaseCoolOffRuleValidator(ISubsidiaryRule subsidiaryRule)
        {
            _subsidiaryRule = subsidiaryRule;
        }

        public async Task<IFeatureResult> ExecuteAsync(FeatureHandle next, IFeatureContext context, IFeatureRequest featureRequest, CancellationToken cancellationToken, params string[] dependentRules)
        {
            var request = featureRequest as IOnboardingJourneyRequest;

            var coolOffRule = await _subsidiaryRule.GetSubsidiaryRuleAsync(Constants.Onboarding, request.CountryCode, RuleName);

            var isApplicable = MiddlewareHelper.CheckIfRuleIsApplicableAsync(request, RuleName, coolOffRule, dependentRules);
            if (!isApplicable)
                return await next(context, request, cancellationToken).ConfigureAwait(false);

            var ruleDefinition = JsonConvert.DeserializeObject<CoolOff>(coolOffRule.ConfigValue.ToString());
            request.Result.PopulateRuleResult(new Rule
            {
                Name = RuleName,
                Code = RuleName,
                Message = nameof(RuleResult.Succeed),
                Result = nameof(RuleResult.Succeed),
                ResultValue = ruleDefinition.CustomerLevel,
                TerminateOnFailure = coolOffRule.TerminateOnFailure
            });

            return await next(context, request, cancellationToken).ConfigureAwait(false);
        }
    }

    public class CoolOff
    {
        public int CustomerLevel { get; set; }
    }
}