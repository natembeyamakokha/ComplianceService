using RestEase;
using Compliance.Infrastructure.Services.WorldCheck;

namespace Compliance.Infrastructure.Proxy;

public interface IWorldCheckApi
{
    [Post("/screeningRequest")]
    Task<WorldCheckResponse> ValidateCustomerFraudStatusAsync(
        [Header("Date")] string date,
        [Header("Content-Type")] string contentType,
        [Header("Authorization")] string authorization,
        [Header("Content-Length")] int contentLength,
        [Body] string request,
        CancellationToken cancellationToken);

    [Get("/{caseSystemId}/results")]
    Task<List<NameResultResponse>> ValidateResultAsync(
        [Header("Date")] string date,
        [Header("Authorization")] string authorization,
        [Path] string caseSystemId, CancellationToken cancellationToken);
}