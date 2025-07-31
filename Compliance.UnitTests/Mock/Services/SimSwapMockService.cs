using Compliance.Application.Contracts;
using Compliance.Domain.Enum;
using Compliance.Domain.Form;
using Compliance.Domain.Response;
using Compliance.Infrastructure.Services;
using Compliance.Shared;
using Omni.Factory;
using Omni.Features;
using System;
using System.Threading.Tasks;

namespace Compliance.UnitTests.Mock.Services;

public class SimSwapMockService : ISimSwapService
{
    private readonly IMapper _mapper;
    private readonly SimSwapRequestFactory _simSwapRequestFactory;

    public SimSwapMockService(IMapper mapper, SimSwapRequestFactory simSwapRequestFactory)
    {
        _mapper = mapper;
        _simSwapRequestFactory = simSwapRequestFactory;
    }

    public Task<Result<SimSwapResponse>> GetLastSwappedDateAsync(string phoneNumber, CountryCode countryCode, Telco telco, int withinTheLastDays = 180)
    {
        var request = CreateSimSwapRequest(phoneNumber, countryCode);
        var simSwapRequest = _simSwapRequestFactory.CreateTelcoProviderRequest(request, telco);

        var context = FeatureContext.Create(request);

        var result = new SimSwapResponse
        {
            ApiReached = true,
            IsSuccessful = true,
            IsSwaped = false,
            LastSwap = DateTime.UtcNow.AddMonths(-10)
        };

        return Task.FromResult(Result<SimSwapResponse>.Success(result));
    }

    private static SimSwapRequest CreateSimSwapRequest(string phoneNumber, CountryCode countryCode)
    {
        return new SimSwapRequest
        {
            RequestId = Guid.NewGuid().ToString(),
            PhoneNumber = phoneNumber,
            CountryCode = countryCode
        };
    }
}
