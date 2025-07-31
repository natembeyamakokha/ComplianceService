using Omni.Features;
using Newtonsoft.Json;
using Omni.Middlewares;
using Compliance.Domain.Form;
using Compliance.Domain.Response;

namespace Compliance.Infrastructure.Providers.Onboarding.BaseMiddlewares
{
    public class BaseMaximumAgeRuleValidator : IBaseMiddlewareValidator
    {
        public string RuleName => nameof(MaximumAge);
        private readonly ISubsidiaryRule _subsidiaryRule;

        public BaseMaximumAgeRuleValidator(ISubsidiaryRule subsidiaryRule)
        {
            _subsidiaryRule = subsidiaryRule;
        }

        public async Task<IFeatureResult> ExecuteAsync(FeatureHandle next, IFeatureContext context, IFeatureRequest featureRequest, CancellationToken cancellationToken, params string[] dependentRules)
        {
            var request = featureRequest as IOnboardingJourneyRequest;

            var maximumAgeRule = await _subsidiaryRule.GetSubsidiaryRuleAsync(Constants.Onboarding, request.CountryCode, RuleName);

            var isApplicable = MiddlewareHelper.CheckIfRuleIsApplicableAsync(request, RuleName, maximumAgeRule, dependentRules);
            if (!isApplicable)
                return await next(context, request, cancellationToken).ConfigureAwait(false);

            var ruleDefinition = JsonConvert.DeserializeObject<MaximumAge>(maximumAgeRule.ConfigValue.ToString());

            if (request.Age > ruleDefinition.MaxAge)
            {
                request.Result.PopulateRuleResult(new Rule
                {
                    Name = RuleName,
                    Code = RuleName,
                    Message = RuleResult.Failed.ToString(),
                    Result = RuleResult.Failed.ToString(),
                    TerminateOnFailure = maximumAgeRule.TerminateOnFailure
                });
                return await next(context, request, cancellationToken).ConfigureAwait(false);
            }

            request.Result.PopulateRuleResult(new Rule
            {
                Name = RuleName,
                Code = RuleName,
                Message = RuleResult.Succeed.ToString(),
                Result = RuleResult.Succeed.ToString()
            });

            return await next(context, request, cancellationToken).ConfigureAwait(false);
        }
    }

    public class MaximumAge
    {
        public int MaxAge { get; set; }
    }
}