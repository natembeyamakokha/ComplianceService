using Omni.Factory;
using Compliance.Domain.Enum;
using Compliance.Domain.Form;
using Compliance.Infrastructure.Providers;

namespace Compliance.Infrastructure.Services;

public class SimSwapRequestFactory
{
    private readonly IMapper _mapper;

    public SimSwapRequestFactory(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public dynamic CreateTelcoProviderRequest(SimSwapRequest request, Telco telco)
    {       
        var selector = new SimSwapSelector(request.CountryCode, telco);
        var mapperResult = _mapper.TryMap(request, selector, out dynamic simSwapRequest);
        return simSwapRequest;
    }
}