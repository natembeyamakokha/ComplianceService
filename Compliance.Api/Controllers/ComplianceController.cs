using MediatR;
using Microsoft.AspNetCore.Mvc;
using Compliance.Application.UseCases.IndividualScreening;
using Compliance.Application.UseCases.OrganisationScreening;
using Compliance.Application.UseCases.VesselScreening;
using Compliance.Domain.Form.Compliance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Compliance.Api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ComplianceController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Full individual screening with secondary fields (DOB, nationality, document, etc.)
    /// </summary>
    [HttpPost("individual-screening")]
    public async Task<IActionResult> ScreenIndividualAsync([FromBody] IndividualScreeningRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request?.Name))
            return BadRequest("Name is required.");

        var command = new IndividualScreeningCommand
        {
            BankId = request.BankId,
            CaseId = request.CaseId,
            CustomerName = request.Name,
            Gender = request.Gender,
            Nationality = request.Nationality,
            DateOfBirth = request.DateOfBirth,
            PlaceOfBirth = request.PlaceOfBirth,
            CountryLocation = request.CountryLocation,
            DocumentId = request.DocumentId,
            DocumentIdType = request.DocumentIdType,
            DocumentIdCountry = request.DocumentIdCountry
        };

        var result = await _mediator.Send(command);
        return result.Successful ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Screen an organisation with registered country and document details
    /// </summary>
    [HttpPost("organisation-screening")]
    public async Task<IActionResult> ScreenOrganisationAsync([FromBody] OrganisationScreeningRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request?.Name))
            return BadRequest("Organisation name is required.");

        var command = new OrganisationScreeningCommand
        {
            BankId = request.BankId,
            CaseId = request.CaseId,
            OrganisationName = request.Name,
            RegisteredCountry = request.RegisteredCountry,
            DocumentId = request.DocumentId,
            DocumentIdType = request.DocumentIdType,
            DocumentIdCountry = request.DocumentIdCountry
        };

        var result = await _mediator.Send(command);
        return result.Successful ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Screen a vessel by IMO number (7-digit numeric)
    /// </summary>
    [HttpPost("vessel-screening")]
    public async Task<IActionResult> ScreenVesselAsync([FromBody] VesselScreeningRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request?.Name))
            return BadRequest("Vessel name is required.");

        if (!IsValidIMONumber(request.IMONumber))
            return BadRequest("IMO Number must be a 7-digit number.");

        var command = new VesselScreeningCommand
        {
            BankId = request.BankId,
            CaseId = request.CaseId,
            VesselName = request.Name,
            IMONumber = request.IMONumber
        };

        var result = await _mediator.Send(command);
        return result.Successful ? Ok(result) : BadRequest(result);
    }

    // Helper: Validate IMO Number (7 digits)
    private static bool IsValidIMONumber(string imoNumber)
    {
        return !string.IsNullOrWhiteSpace(imoNumber) &&
               System.Text.RegularExpressions.Regex.IsMatch(imoNumber, @"^[0-9]{7}$");
    }
}