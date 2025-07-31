using MediatR;
using Compliance.Domain.Form;
using Microsoft.AspNetCore.Mvc;
using Compliance.Application.UseCases.Onboarding.VerifyUserDetails;

namespace Compliance.Api;

public class RuleValidationController : BaseController
{
    private readonly IMediator _mediator;

    public RuleValidationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("onboarding")]
    public async Task<IActionResult> ValidateUserDetailsAsync([FromBody] OnboardingJourneyForm request)
    {
        request.Validate();
        return Ok(await _mediator.Send(new VerifyUserDetailsCommand(request.Age, request.Channel, request.CountryCodeEnum, request.PhoneNumbers, request.CustomerName)));
    }
}