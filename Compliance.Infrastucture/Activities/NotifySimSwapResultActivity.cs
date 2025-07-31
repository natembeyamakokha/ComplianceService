using Compliance.Application.UseCases.SimSwap.ProcessNotifySimSwapResult;
using Compliance.Infrastructure.ApiClient;
using Newtonsoft.Json;
using Omni;

namespace Compliance.Infrastructure.Activities;

internal class NotifySimSwapResultActivity : INotifySimSwapResultActivity
{
    private readonly IAPIWrapper<NotifyCallerSimSwapResultRequest, Shared.Result> _apiWrapper;

    public NotifySimSwapResultActivity(IAPIWrapper<NotifyCallerSimSwapResultRequest, Shared.Result> apiWrapper)
    {
        _apiWrapper = apiWrapper;
    }

    public async Task<Result<string>> NotifyCallerSimSwapResultAsync(NotifyCallerSimSwapResultRequest request, CancellationToken cancellationToken)
    {
        var response = await _apiWrapper.PostAsync(request, request.CallbackUrl);

        var serializedResponse = JsonConvert.SerializeObject(response);

        if (!response.IsSuccessful)
        {
            return new Error(serializedResponse, Shared.StatusCodes.INVALID_REQUEST, true);
        }

        return serializedResponse;
    }
}