using Omni.CQRS.Commands;

namespace Compliance.Application.UseCases.SimSwap.ProcessNotifySimSwapResult;

public record ProcessNotifySimSwapResultCommand(string URL, string Payload) : CommandBase<string>(Guid.NewGuid()), IRecurringCommand;