using Omni.Features;
using Omni.Middlewares;

namespace Compliance.Infrastructure.Providers.Onboarding.BaseMiddlewares
{
    public interface IBaseMiddlewareValidator
    {
        public string RuleName { get; }
        Task<IFeatureResult> ExecuteAsync(FeatureHandle next, IFeatureContext context, IFeatureRequest request, CancellationToken cancellationToken, params string[] dependentRules);
    }
}