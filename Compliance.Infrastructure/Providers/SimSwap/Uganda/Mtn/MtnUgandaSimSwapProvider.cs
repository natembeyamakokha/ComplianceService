using Omni.Features;
using Omni.Providers;
using Microsoft.Extensions.Logging;
using Compliance.Infrastructure.ApiClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Distributed;

namespace Compliance.Infrastructure.Providers.Uganda.Mtn
{
    internal sealed class MtnUgandaSimSwapProvider : FeatureProvider<MtnUgandaSimSwapRequest, MtnUgandaSimSwapResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _cacheService;
        private readonly ILogger<MtnUgandaSimSwapProvider> _logger;
        private readonly IAPIWrapper<MtnUgandaSimSwapRequest, MtnUgandaSimSwapResponse> _apiWrapper;

        public MtnUgandaSimSwapProvider(IConfiguration configuration, ILogger<MtnUgandaSimSwapProvider> logger, IAPIWrapper<MtnUgandaSimSwapRequest, MtnUgandaSimSwapResponse> apiWrapper, IDistributedCache cacheService)
        {
            _logger = logger;
            _apiWrapper = apiWrapper;
            _cacheService = cacheService;
            _configuration = configuration;
        }

        protected override async Task<FeatureResult<MtnUgandaSimSwapResponse>> ExecuteAsync(MtnUgandaSimSwapRequest request, IFeatureContext context)
        {
            var tranId = Guid.NewGuid();
            try
            {
                string url = _configuration[$"UG:MTN:URL"];
                url = url.Replace("{{mobileNumberWithOutDialCode}}", request.PhoneNumber);
                string apiKey = _configuration[$"UG:MTN:ApiKey"];

                var headers = new List<KeyValuePair<string, string>>();
                headers.Add(new KeyValuePair<string, string>("transactionId", $"{tranId}"));
                headers.Add(new KeyValuePair<string, string>("x-api-key", $"{apiKey}"));
                headers.Add(new KeyValuePair<string, string>("Content-Type", "application/json"));

                _logger.LogInformation($"Starting sim swap check for {request.PhoneNumber} at {DateTime.UtcNow}");

                var result = await _apiWrapper.GetAsync(url, headers);

                if (result.IsSuccessful)
                {
                    result.Data.ApiReached = true;
                    return FeatureResult.Succeed(request, result.Data);
                }

                _logger.LogInformation($"Finished sim swap check for {request.PhoneNumber} at {DateTime.UtcNow} with lastSwapDate {result.Content}");
                return FeatureResult.Fail(request, result.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"{ex.Message} {request.PhoneNumber}");
                return FeatureResult.Fail(request, ex.Message);
            }
        }
    }
}