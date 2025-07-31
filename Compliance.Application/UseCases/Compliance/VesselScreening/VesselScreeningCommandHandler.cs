using Compliance.Shared;
using MediatR;

namespace Compliance.Application.UseCases.Compliance.VesselScreening
{
    public class VesselScreeningCommandHandler(
        IVesselScreeningActivity iVesselScreeningActivity)
        : IRequestHandler<VesselScreeningCommand, Result<VesselScreeningResult>>
    {
        private readonly IVesselScreeningActivity _activity = iVesselScreeningActivity;

        public async Task<Result<VesselScreeningResult>> Handle(
            VesselScreeningCommand request,
            CancellationToken cancellationToken)
        {
            var result = await _activity.VesselProcessScreeningAsync(request);

            if (result.Result == null)
            {
                return Result<VesselScreeningResult>.Failure("Screening process returned no result.");
            }

            return Result<VesselScreeningResult>.Success(result, "Screening completed successfully.");
        }
    }
}