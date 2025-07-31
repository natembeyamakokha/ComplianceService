namespace Compliance.Application.UseCases.Compliance.VerifyFraudStatus;

public interface IVerifyFraudStatusActivity
{
    Task<Omni.Result<bool>> VerifyUserFraudStatusAsync(string customerName,string groupId, string entityType, CancellationToken cancellationToken);
}