using Compliance.Shared;
using Compliance.Application.Contracts.Handlers;
using Compliance.Application.Settings;

namespace Compliance.Application.UseCases.Compliance.VerifyFraudStatus;

public class VerifyFraudStatusCommandHandler(IVerifyFraudStatusActivity verifyFraudStatusActivity, CaseScreeningSettings caseScreeningSettings) : ICommandHandler<VerifyFraudStatusCommand, Result<VerifyFraudStatusResult>>
{
    private readonly IVerifyFraudStatusActivity _verifyFraudStatusActivity = verifyFraudStatusActivity;
    private readonly CaseScreeningSettings _caseScreeningSettings = caseScreeningSettings;

    public async Task<Result<VerifyFraudStatusResult>> Handle(VerifyFraudStatusCommand command, CancellationToken cancellationToken)
    {
        var result = await _verifyFraudStatusActivity.VerifyUserFraudStatusAsync(command.Name, _caseScreeningSettings.GroupId,  command.EntityType, cancellationToken);

        if (result.HasError)
            return VerifyFraudStatusResult.Failure(result?.Error?.FullMessage);

        return VerifyFraudStatusResult.Success(new VerifyFraudStatusResult
        {
            IsCompliant = result.Value
        });
    }
}