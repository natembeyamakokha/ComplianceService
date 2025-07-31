using Omni;

namespace Compliance.Application.UseCases.SimSwap.ProcessNotifySimSwapResult;

public interface INotifySimSwapResultActivity
{
    Task<Result<string>> NotifyCallerSimSwapResultAsync(NotifyCallerSimSwapResultRequest request, CancellationToken cancellationToken);
}