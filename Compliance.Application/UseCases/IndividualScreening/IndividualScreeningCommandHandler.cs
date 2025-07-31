using Compliance.Shared;
using MediatR;

namespace Compliance.Application.UseCases.IndividualScreening
{
    public class IndividualScreeningCommandHandler(IIndividualScreeningActivity iIndividualScreeningActivity) : IRequestHandler<IndividualScreeningCommand, Result<IndividualScreeningResult>>
    {
        private readonly IIndividualScreeningActivity _activity = iIndividualScreeningActivity;

        public async Task<Result<IndividualScreeningResult>> Handle(IndividualScreeningCommand request, CancellationToken cancellationToken)
        {
            var result = await _activity.IndividualProcessScreeningAsync(request);

            if (result.Result == null)
            {
                return Result<IndividualScreeningResult>.Failure("Failed");
            }

            return Result<IndividualScreeningResult>.Success(result, "Successful");
        }
    }
}
