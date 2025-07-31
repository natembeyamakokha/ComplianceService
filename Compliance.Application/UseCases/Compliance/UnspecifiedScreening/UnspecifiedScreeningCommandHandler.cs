using Compliance.Shared;
using MediatR;

namespace Compliance.Application.UseCases.Compliance.UnspecifiedScreening
{
    public class UnspecifiedScreeningCommandHandler(
        IUnspecifiedScreeningActivity iUnspecifiedScreeningActivity)
        : IRequestHandler<UnspecifiedScreeningCommand, Result<UnspecifiedScreeningResult>>
    {
        private readonly IUnspecifiedScreeningActivity _activity = iUnspecifiedScreeningActivity;

        public async Task<Result<UnspecifiedScreeningResult>> Handle(
            UnspecifiedScreeningCommand request,
            CancellationToken cancellationToken)
        {
            var result = await _activity.UnspecifiedProcessScreeningAsync(request);

            if (result.Result == null)
            {
                return Result<UnspecifiedScreeningResult>.Failure("Screening process returned no result.");
            }

            return Result<UnspecifiedScreeningResult>.Success(result, "Screening completed successfully.");
        }
    }
}