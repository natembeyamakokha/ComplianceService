using Compliance.Domain.Response;

namespace Compliance.Application.UseCases.SimSwap.GetSimSwapStatus;

public record SimSwapStatusQueryResponse(Guid TaskId, string Status, BulkSimSwapResponse ResultPayload, string ErrorMessage);