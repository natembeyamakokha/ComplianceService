using Compliance.Domain.Entity.Interfaces;
using Compliance.Domain.Enum;
using Compliance.Domain.Response;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Omni;
using Omni.CQRS.Queries;

namespace Compliance.Application.UseCases.SimSwap.GetSimSwapStatus;

internal sealed class GetSimSwapStatusQueryHandler : IQueryHandler<GetSimSwapTaskStatusQuery, SimSwapStatusQueryResponse>
{
    private readonly ISimSwapCheckTaskRepository _swapCheckTaskRepository;
    private readonly ILogger<GetSimSwapStatusQueryHandler> _logger;

    public GetSimSwapStatusQueryHandler(
        ISimSwapCheckTaskRepository swapCheckTaskRepository,
        ILogger<GetSimSwapStatusQueryHandler> logger)
    {
        _swapCheckTaskRepository = swapCheckTaskRepository;
        _logger = logger;
    }

    public async Task<Result<SimSwapStatusQueryResponse>> Handle(GetSimSwapTaskStatusQuery query, CancellationToken cancellationToken)
    {
        var task = await _swapCheckTaskRepository.FindSingleAsync(query.TaskId, cancellationToken: cancellationToken);

        if (task == null) 
        {
            return new Error(Shared.ErrorMessages.SimSwapTaskNotFound, Shared.StatusCodes.INVALID_REQUEST, false);
        }

        BulkSimSwapResponse simSwapResult = default;

        if (task.TaskStatusDescription == nameof(SimSwapCheckTaskStatus.Completed)) 
        {
            try
            {
                simSwapResult = JsonConvert.DeserializeObject<BulkSimSwapResponse>(task.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new Error(Shared.ErrorMessages.UnableToParse, Shared.StatusCodes.INVALID_REQUEST, false);
            }
        }

        var response = new SimSwapStatusQueryResponse(task.Id, task.TaskStatusDescription, simSwapResult, task.ErrorMessage);

        return response;
    }
}
