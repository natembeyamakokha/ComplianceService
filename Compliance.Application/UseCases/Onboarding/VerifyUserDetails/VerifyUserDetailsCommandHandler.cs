using Compliance.Shared;
using Compliance.Domain.Form;
using Compliance.Domain.Response;
using Compliance.Application.Contracts;
using Compliance.Application.Contracts.Handlers;

namespace Compliance.Application.UseCases.Onboarding.VerifyUserDetails;

public class VerifyUserDetailsCommandHandler : ICommandHandler<VerifyUserDetailsCommand, Result<OnboardingJourneyResponse>>
{
    private readonly IOnboardingModule _onboardingModule;
    public VerifyUserDetailsCommandHandler(IOnboardingModule onboardingModule)
    {
        _onboardingModule = onboardingModule;
    }

    public async Task<Result<OnboardingJourneyResponse>> Handle(VerifyUserDetailsCommand command, CancellationToken cancellationToken)
    {        
        var onboardingJourneycommand = OnboardingJourneyRequest.Create(
            command.Age, 
            command.Channel, 
            command.CountryCode, 
            command.PhoneNumbers, 
            customerName : command.CustomerName);
        
        var result = await _onboardingModule.ApplyAsync(onboardingJourneycommand, cancellationToken);
        return VerifyUserDetailsResult.Success(result);
    }
}
