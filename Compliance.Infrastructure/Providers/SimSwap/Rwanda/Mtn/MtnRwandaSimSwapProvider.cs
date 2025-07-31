using Omni.Features;
using Omni.Providers;
using Microsoft.Extensions.Logging;
using Compliance.Infrastructure.ApiClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Distributed;

namespace Compliance.Infrastructure.Providers.Rwanda.Mtn
{
    internal sealed class MtnRwandaSimSwapProvider : FeatureProvider<MtnRwandaSimSwapRequest, MtnRwandaSimSwapResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _cacheService;
        private readonly ILogger<MtnRwandaSimSwapProvider> _logger;
        private readonly IAPIWrapper<MtnRwandaSimSwapRequest, MtnRwandaSimSwapResponse> _apiWrapper;

        public MtnRwandaSimSwapProvider(IConfiguration configuration, ILogger<MtnRwandaSimSwapProvider> logger, IAPIWrapper<MtnRwandaSimSwapRequest, MtnRwandaSimSwapResponse> apiWrapper, IDistributedCache cacheService)
        {
            _logger = logger;
            _apiWrapper = apiWrapper;
            _cacheService = cacheService;
            _configuration = configuration;
        }

        protected override async Task<FeatureResult<MtnRwandaSimSwapResponse>> ExecuteAsync(MtnRwandaSimSwapRequest request, IFeatureContext context)
        {
            string url = _configuration["RW:MTN:URL"];
            string key = _configuration["RW:MTN:URL"];
            string transactionId = _configuration["RW:MTN:URL"];

            var headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("countrycode", "250"));
            headers.Add(new KeyValuePair<string, string>("transactionid", $"{transactionId}"));
            headers.Add(new KeyValuePair<string, string>("x-api-key", $"{key}"));

            try
            {
                var response = await _apiWrapper.GetAsync($"{url}/productOrder?MSISDN={request.PhoneNumber}&orderType=66", headers);

                if (response?.IsSuccessful == true)
                {
                    var lastSwappedDate = response.Data.Data.OrderByDescending(x => x.CompletionDate).Select(x => x.CompletionDate).FirstOrDefault();
                    return FeatureResult.Succeed(request, new()
                    {
                        ApiReached = true,
                        LastSwap = lastSwappedDate
                    });
                }
                else
                    return FeatureResult.Fail(request, "99");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return FeatureResult.Fail(request, ex.Message);
            }

        }
    }
}