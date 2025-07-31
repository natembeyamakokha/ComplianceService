using Compliance.Application.UseCases.VesselScreening;

namespace Compliance.Infrastructure.Activities
{
    public class VesselScreeningActivity : IVesselScreeningActivity
    {
        public Task<VesselScreeningResult> VesselProcessScreening(VesselScreeningCommand request)
        {
            throw new System.NotImplementedException();
        }
    }
}
