using Omni;
using Omni.Factory;
using Compliance.Domain.Form;

namespace Compliance.Infrastructure.Providers.Onboarding
{
    public class OnboardingJourneyMapperFactory
    {
        private readonly IMapper _mapper;

        public OnboardingJourneyMapperFactory(IMapper mapper) => _mapper = mapper;

        public dynamic CreateOnboardingJourneyRequest(OnboardingJourneyRequest request)
        {
            var selector = new OnboardingSelector(request.CountryCode);
            var mapperResult = _mapper.TryMap(request, selector, out dynamic onboardingRequest);
          
            if (mapperResult.HasError)
                return new NullError(nameof(onboardingRequest));

            return onboardingRequest;
        }
    }
}
