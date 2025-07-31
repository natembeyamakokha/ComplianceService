using Compliance.Domain.Response;

namespace Compliance.Application.UseCases.SimSwap.ProcessNotifySimSwapResult;

public record NotifyCallerSimSwapResultRequest(string CallbackUrl, BulkSimSwapResponse Response);
