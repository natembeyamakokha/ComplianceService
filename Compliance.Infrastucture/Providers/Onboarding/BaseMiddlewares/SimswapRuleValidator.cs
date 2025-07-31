using Autofac;
using MediatR;
using Omni.Features;
using Newtonsoft.Json;
using Omni.Middlewares;
using Compliance.Domain.Form;
using Compliance.Infrastructure;
using Compliance.Domain.Response;
using Compliance.Infrastructure.Providers.Onboarding.Kenya;
using Compliance.Domain.Entity.Interfaces;

namespace Compliance.Infrastructure.Providers.Onboarding.BaseMiddlewares
{
    public class BaseSimswapRuleValidator : IBaseMiddlewareValidator
    {
        public string RuleName => nameof(Simswap);
        private readonly ISubsidiaryRule _subsidiaryRule;

        public BaseSimswapRuleValidator(ISubsidiaryRule subsidiaryRule)
        {
            _subsidiaryRule = subsidiaryRule;
        }

        public async Task<IFeatureResult> ExecuteAsync(FeatureHandle next, IFeatureContext context, IFeatureRequest featureRequest, CancellationToken cancellationToken, params string[] dependentRules)
        {
            var request = featureRequest as IOnboardingJourneyRequest;

            var simSwapRule = await _subsidiaryRule.GetSubsidiaryRuleAsync(Constants.Onboarding, request.CountryCode, RuleName);

            var isApplicable = MiddlewareHelper.CheckIfRuleIsApplicableAsync(request, RuleName, simSwapRule, dependentRules);

            if (!isApplicable)
                return await next(context, request, cancellationToken).ConfigureAwait(false);

            var ruleDefinition = JsonConvert.DeserializeObject<Simswap>(simSwapRule.ConfigValue.ToString());

            using var scope = ServiceCompositionRoot.BeginLifetimeScope();

            var simSwapCheckTaskRepository = scope.Resolve<ISimSwapCheckTaskRepository>();

            var simswapCheckTaskResult = request.CreateSimSwapTaskEntity(ruleDefinition.AllowedNumberOfDays);

            if (simswapCheckTaskResult.HasError) 
            {
                request.Result.PopulateRuleResult(new Rule
                {
                    Name = RuleName,
                    Code = RuleName,
                    Message = nameof(RuleResult.Failed),
                    Result = nameof(RuleResult.Failed),
                    ResultValue = simswapCheckTaskResult.Error
                });
            }

            // TODO: handle exception
            try
            {
                await simSwapCheckTaskRepository.AddAndSaveChangesAsync(simswapCheckTaskResult.Value, cancellationToken);
            }
            catch(Exception ex)
            { }

            request.Result.PopulateRuleResult(new Rule
            {
                Name = RuleName,
                Code = RuleName,
                Message = nameof(RuleResult.Deferred),
                Result = nameof(RuleResult.Deferred)
            });

            return await next(context, request, cancellationToken).ConfigureAwait(false);
        }
    }

    public class Simswap
    {
        public int AllowedNumberOfDays { get; set; }
    }
}