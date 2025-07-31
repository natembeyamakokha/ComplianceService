using Omni;

namespace Compliance.Infrastructure.Services.WorldCheck;

public interface IWorldCheckService
{
    Task<Result<bool>> ValidateCustomerFraudStatusAsync(WorldCheckRequests request, CancellationToken cancellationToken);
}
