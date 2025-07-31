namespace Compliance.Infrastructure.Services.WorldCheck.IndividualScreeningRequestAndResponse;

public interface IIndividualWorldCheckScreeningService
{
    Task<WorldCheckResponse> ScreenIndividualAsync(IndividualWorldCheckScreeningRequest request);
}
