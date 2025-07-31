using Compliance.Domain.Response;
using Newtonsoft.Json;
using Omni;
using Omni.CQRS.Commands;

namespace Compliance.Application.UseCases.SimSwap.ProcessNotifySimSwapResult;

internal sealed class ProcessNotifySimSwapResultCommandHandler : ICommandHandler<ProcessNotifySimSwapResultCommand, string>
{
    private readonly INotifySimSwapResultActivity _activity;

    public ProcessNotifySimSwapResultCommandHandler(INotifySimSwapResultActivity activity)
    {
        _activity = activity;    
    }

    public async Task<Result<string>> Handle(ProcessNotifySimSwapResultCommand command, CancellationToken cancellationToken)
    {
        BulkSimSwapResponse simSwapResult;

        try
        {
            if (string.IsNullOrEmpty(command.Payload)) 
            {
                return new Error(Shared.ErrorMessages.UnableToParse, Shared.StatusCodes.INVALID_REQUEST, false);
            }

            simSwapResult = JsonConvert.DeserializeObject<BulkSimSwapResponse>(command.Payload);
        }
        catch (Exception) 
        {
            return new Error(Shared.ErrorMessages.UnableToParse, Shared.StatusCodes.INVALID_REQUEST, false);
        }
       
        var request = new NotifyCallerSimSwapResultRequest(command.URL, simSwapResult);

        var response = await _activity.NotifyCallerSimSwapResultAsync(request, cancellationToken);

        if (response.HasError) 
        {
            return new Error(response.Error.FullMessage, Shared.StatusCodes.INVALID_REQUEST, true);
        }

        return response.Value;
    }
}
