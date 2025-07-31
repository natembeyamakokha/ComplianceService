using Omni.CQRS.Commands;

namespace Compliance.Application.UseCases.SimSwap.ProcessVerifySimSwap;

public record ProcessVerifySimSwapCommand(
    int AllowedSwappedDaysCount, 
    string CountryCode,
    IReadOnlyCollection<string> PhoneNumbers,
    string CallbackUrl,
    string Cif) 
    : CommandBase<string>(Guid.NewGuid()), IRecurringCommand;