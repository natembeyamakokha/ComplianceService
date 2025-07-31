using Omni.CQRS.Queries;

namespace Compliance.Application.UseCases.SimSwap.GetSimSwapStatus;

public record GetSimSwapTaskStatusQuery(Guid TaskId) : QueryBase<SimSwapStatusQueryResponse>(Guid.NewGuid())
{
}