using Compliance.Infrastructure.Services.WorldCheck.Requests;
using Compliance.Infrastructure.Services.WorldCheck.Responses;
using System.Threading.Tasks;

namespace Compliance.Infrastructure.Services.WorldCheck
{
    public class WorldCheckService : IWorldCheckService
    {
        public Task<WorldCheckResponse> ScreenIndividualAsync(WorldCheckScreeningRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Task<WorldCheckResponse> ScreenOrganisationAsync(WorldCheckScreeningRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Task<WorldCheckResponse> ScreenVesselAsync(WorldCheckScreeningRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
