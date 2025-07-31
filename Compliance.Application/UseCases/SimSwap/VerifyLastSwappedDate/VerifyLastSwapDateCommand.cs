using Compliance.Shared;
using Compliance.Domain.Enum;
using Compliance.Domain.Form;
using Compliance.Domain.Response;
using Compliance.Application.Commands;

namespace Compliance.Application.UseCases.VerifyLastSwapDate;

public record VerifyLastSwapDateCommand(string PhoneNumber, CountryCode CountryCode, int AllowedDaysCount = 180) : CommandBase<Result<SimSwapResponse>>
{}

public record VerifyLastSwapDateMultipleCommand(List<SimSwapForm> SimSwapRequests) : CommandBase<Result<BulkSimSwapResponse>>
{}


