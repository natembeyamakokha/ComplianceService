using Compliance.Infrastructure.Services.WorldCheck.IndividualScreeningRequestAndResponse;
using RestEase;

namespace Compliance.Infrastructure.Proxy
{
    public interface IIndividualWorldCheckApi
    {
        [Post("/screeningRequest")]
        Task<IndividualWorldCheckScreeningResponse> ValidateIndividualWorldCheckStatusAsync(
        [Header("Date")] string date,
        [Header("Content-Type")] string contentType,
        [Header("Authorization")] string authorization,
        [Header("Content-Length")] int contentLength,
        [Body] string request);
    }
}
