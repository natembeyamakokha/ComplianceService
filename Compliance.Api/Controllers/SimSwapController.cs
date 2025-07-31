using MediatR;
using Compliance.Domain.Form;
using Microsoft.AspNetCore.Mvc;
using Compliance.Application.UseCases.VerifyLastSwapDate;
using Compliance.Application.UseCases.SimSwap.GetSimSwapStatus;

namespace Compliance.Api;

public class SimSwapController : BaseController
{
    private readonly IMediator _mediator;

    public SimSwapController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("verify")]
    public async Task<IActionResult> GetLastSwappedDateForMultipleNumberAsync([FromBody] List<SimSwapForm> request)
    {
        request?.ForEach(x => x.Validate());
        return Ok(await _mediator.Send(new VerifyLastSwapDateMultipleCommand(request)));
    }

    [HttpGet("status")]
    public async Task<IActionResult> GetSimSwapTaskStatusAsync([FromQuery] Guid taskId, CancellationToken cancellationToken)
    {
        var query = new GetSimSwapTaskStatusQuery(taskId);

        var result = await _mediator.Send(query, cancellationToken);

        if (result.HasError) 
        {
            return BadRequest(Shared.Result<SimSwapStatusQueryResponse>.Failure(result.Error.FullMessage));
        }

        return Ok(Shared.Result<SimSwapStatusQueryResponse>.Success(result.Value));
    }
}
