using Compliance.Domain.Form;
using Compliance.Domain.Mappers;
using Compliance.Domain.Response;

namespace Compliance.Infrastructure.Providers.Onboarding.BaseMiddlewares
{
    public static class MiddlewareHelper
    {
        public static bool CheckIfRuleIsApplicableAsync(IOnboardingJourneyRequest request, string ruleName, SubsidiaryRulesModel middlewareRules, params string[] dependentRules)
        {
            bool isApplicable = false;

            if (request.Result.ShouldTerminate())
            {
                request.Result.PopulateRuleResult(new Rule
                {
                    Name = ruleName,
                    Code = ruleName,
                    Message = nameof(RuleResult.NotApplied),
                    Result = nameof(RuleResult.NotApplied)
                });

                return isApplicable;
            }


            if (!request.Result.CanRunRuleBasedDependencyRuleResult(dependentRules))
            {
                request.Result.PopulateRuleResult(new Rule
                {
                    Name = ruleName,
                    Code = ruleName,
                    Message = nameof(RuleResult.NotApplied),
                    Result = nameof(RuleResult.NotApplied)
                });

                return isApplicable;
            }

            if (middlewareRules is null)
            {
                request.Result.PopulateRuleResult(new Rule
                {
                    Name = ruleName,
                    Code = ruleName,
                    Message = nameof(RuleResult.NotApplied),
                    Result = nameof(RuleResult.NotApplied)
                });
                return isApplicable;
            }

            if (!middlewareRules.IsApplicable)
            {
                request.Result.PopulateRuleResult(new Rule
                {
                    Name = ruleName,
                    Code = ruleName,
                    Message = nameof(RuleResult.NotApplied),
                    Result = nameof(RuleResult.NotApplied)
                });
                return isApplicable;
            }

            isApplicable = true;
            return isApplicable;
        }
    }
}

