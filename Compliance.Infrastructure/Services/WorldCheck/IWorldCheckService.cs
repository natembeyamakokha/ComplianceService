using Compliance.Infrastructure.Services.WorldCheck.Requests;
using Compliance.Infrastructure.Services.WorldCheck.Responses;
using System.Threading.Tasks;

namespace Compliance.Infrastructure.Services.WorldCheck
{
    public interface IWorldCheckService
    {
        Task<WorldCheckResponse> ScreenIndividualAsync(WorldCheckScreeningRequest request);
        Task<WorldCheckResponse> ScreenOrganisationAsync(WorldCheckScreeningRequest request);
        Task<WorldCheckResponse> ScreenVesselAsync(WorldCheckScreeningRequest request);
    }
}
