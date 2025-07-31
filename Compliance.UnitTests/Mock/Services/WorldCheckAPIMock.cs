using Moq;
using Compliance.Infrastructure.Proxy;
using System.Threading.Tasks;
using Omni.Utilities.ResponseFactories.Models;
using Compliance.Infrastructure.Services.WorldCheck;
using RestEase;
using System.Threading;
using System.Collections.Generic;

namespace Compliance.UnitTests.Mock.Services;

public class WorldCheckAPIMock : IWorldCheckApi
{
    public const string VALID_NAME = "WorldCheckSuccess";
    public const string INVALID_NAME = "WorldCheckFailure";
    public const string ERROR_NAME = "WorldCheckError";

    public Mock<IWorldCheckApi> MockedWorldCheckAPI { get; } = new Mock<IWorldCheckApi>();

    public WorldCheckAPIMock()
    {
    }

    public Task<ServiceResponse<WorldCheckResponse>> ValidateCustomerFraudStatusAsync([Body] WorldCheckRequests request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<WorldCheckResponse> ValidateCustomerFraudStatusAsync([Header("Date")] string date, [Header("Content-Type")] string contentType, [Header("Authorization")] string authorization, [Header("Content-Length")] int contentLength, [Body] string request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<List<NameResultResponse>> ValidateResultAsync([Header("Date")] string date, [Header("Authorization")] string authorization, [Path] string caseSystemId, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}
