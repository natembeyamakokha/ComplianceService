using Omni;
using Compliance.Infrastructure.Services.WorldCheck;
using Compliance.Application.UseCases.Compliance.VerifyFraudStatus;

namespace Compliance.Infrastructure.Activities;

internal class VerifyFraudStatusActivity(IWorldCheckService worldCheckService) : IVerifyFraudStatusActivity
{
    private readonly IWorldCheckService _worldCheckService = worldCheckService;

    public async Task<Result<bool>> VerifyUserFraudStatusAsync(string customerName,string groupId, string entityType, CancellationToken cancellationToken)
    {
        var result = await _worldCheckService.ValidateCustomerFraudStatusAsync(
            WorldCheckRequests.Create(customerName, groupId, entityType),
            cancellationToken);

        if (result.HasError)
            return result.Error;

        return result.Value;
    }
}
