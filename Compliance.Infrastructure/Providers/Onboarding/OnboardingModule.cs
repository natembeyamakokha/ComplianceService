using Omni;
using Omni.Modules;
using Omni.Features;
using Compliance.Domain.Form;
using Compliance.Domain.Response;
using Compliance.Application.Contracts;

namespace Compliance.Infrastructure.Providers.Onboarding;

internal sealed class OnboardingModule : FeaturesModule<IOnboardingModule>, IOnboardingModule
{
    private readonly OnboardingJourneyMapperFactory _factory;

    public OnboardingModule(IFeaturesModule module, OnboardingJourneyMapperFactory factory) : base(module)
    {
        _factory = factory;
    }

    public async Task<OnboardingJourneyResponse> ApplyAsync(OnboardingJourneyRequest request, CancellationToken cancellationToken)
    {
        var entity = new ApiEntity(request);
        var context = FeatureContext.Create(entity);
        var onboardingRequest = _factory.CreateOnboardingJourneyRequest(request);

        try
        {
            var result = await _module.ExecuteRequestAsync(onboardingRequest, context, cancellationToken);
            return result.Value!;
        }
        catch (Exception ex)
        {
            return default;
        }
    }

    //NOTE: if we intend to capture request on the DB, this is the entity to persist.
    internal sealed record ApiEntity(IOnboardingJourneyRequest Request) : IEntity
    {
        public long Id { get; set; }
        public string Key => Request.Key;
    }
}