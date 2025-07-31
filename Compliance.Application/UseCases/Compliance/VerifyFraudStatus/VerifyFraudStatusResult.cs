using Compliance.Shared;

namespace Compliance.Application.UseCases.Compliance.VerifyFraudStatus;

public class VerifyFraudStatusResult : Result<VerifyFraudStatusResult>
{
    public bool IsCompliant { get; set; }
}
