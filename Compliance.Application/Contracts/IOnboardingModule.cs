using Omni.Modules;
using Compliance.Domain.Form;
using Compliance.Domain.Response;

namespace Compliance.Application.Contracts;

public interface IOnboardingModule : IFeaturesModule
{
    Task<OnboardingJourneyResponse> ApplyAsync(OnboardingJourneyRequest request, CancellationToken cancellationToken);
}