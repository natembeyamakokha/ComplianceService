using Compliance.Shared;
using MediatR;

namespace Compliance.Application.UseCases.OrganisationScreening
{
    public class OrganisationScreeningCommandHandler(
        IOrganisationScreeningActivity iOrganisationScreeningActivity)
        : IRequestHandler<OrganisationScreeningCommand, Result<OrganisationScreeningResult>>
    {
        private readonly IOrganisationScreeningActivity _activity = iOrganisationScreeningActivity;

        public async Task<Result<OrganisationScreeningResult>> Handle(
            OrganisationScreeningCommand request,
            CancellationToken cancellationToken)
        {
            var result = await _activity.OrganisationProcessScreeningAsync(request);

            if (result.Result == null)
            {
                return Result<OrganisationScreeningResult>.Failure("Screening process returned no result.");
            }

            return Result<OrganisationScreeningResult>.Success(result, "Screening completed successfully.");
        }
    }
}