using Compliance.Shared;
using Compliance.Application.Commands;

namespace Compliance.Application.UseCases.Compliance.VerifyFraudStatus;

public record VerifyFraudStatusCommand(string Name, string EntityType) : CommandBase<Result<VerifyFraudStatusResult>>
{ }