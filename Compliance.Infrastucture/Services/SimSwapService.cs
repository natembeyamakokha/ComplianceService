using Autofac;
using Omni.Factory;
using Omni.Modules;
using Omni.Features;
using Compliance.Shared;
using Compliance.Domain.Enum;
using Compliance.Domain.Response;
using Compliance.Application.Contracts;
using Compliance.Domain.Form;

namespace Compliance.Infrastructure.Services;

public class SimSwapService : ISimSwapService
{
    private readonly IMapper _mapper;
    private readonly SimSwapRequestFactory _simSwapRequestFactory;

    public SimSwapService(IMapper mapper, SimSwapRequestFactory simSwapRequestFactory)
    {
        _mapper = mapper;
        _simSwapRequestFactory = simSwapRequestFactory;
    }

    public async Task<Result<SimSwapResponse>> GetLastSwappedDateAsync(string phoneNumber, CountryCode countryCode, Telco telco, int withinTheLastDays = 180)
    {
        //TODO: Fix middleware excution order. This doesn't need to execute other middlewares
        var request = CreateSimSwapRequest(phoneNumber, countryCode);
        var simSwapRequest = _simSwapRequestFactory.CreateTelcoProviderRequest(request, telco);

        var context = FeatureContext.Create(request);

        using (var scope = ServiceCompositionRoot.BeginLifetimeScope())
        {
            var service = scope.Resolve<IFeaturesModule>();
            var result = await service.ExecuteRequestAsync(simSwapRequest, context);

            //TODO: clean up on response use Omni result
            if (result.Status == ExecutionStatus.Success)
                return Result<SimSwapResponse>.Success(result.Value);

            return Result<SimSwapResponse>.Failure(result.FailureCode);
        }
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