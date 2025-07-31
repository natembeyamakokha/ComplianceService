using System.Net;
using Omni.Features;
using Omni.Providers;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Logging;
using Compliance.Infrastructure.ApiClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Distributed;

namespace Compliance.Infrastructure.Providers.Kenya.Safaricom
{
    internal sealed class SafaricomKenyaSimSwapProvider : FeatureProvider<SafaricomSimSwapRequest, SafaricomKenyaSimSwapResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _cacheService;
        private readonly ILogger<SafaricomKenyaSimSwapProvider> _logger;
        private readonly SafaricomProviderHelper _safaricomProviderHelper;
        private readonly IAPIWrapper<SafaricomSimSwapRequest, object> _apiWrapper;

        public SafaricomKenyaSimSwapProvider(IConfiguration configuration, SafaricomProviderHelper safaricomProviderHelper, ILogger<SafaricomKenyaSimSwapProvider> logger, IAPIWrapper<SafaricomSimSwapRequest, object> apiWrapper, IDistributedCache cacheService)
        {
            _logger = logger;
            _apiWrapper = apiWrapper;
            _cacheService = cacheService;
            _configuration = configuration;
            _safaricomProviderHelper = safaricomProviderHelper;
        }

        protected override async Task<FeatureResult<SafaricomKenyaSimSwapResponse>> ExecuteAsync(SafaricomSimSwapRequest request, IFeatureContext context)
        {
            try
            {
                string endpoint = _configuration["KE:Safaricom:SwapCheckUrl"];
                string token = await _safaricomProviderHelper.GetSwapToken();

                _logger.LogInformation($"Saf Cached Token {token} {request.CustomerNumber}");

                if (string.IsNullOrWhiteSpace(token))
                    return FeatureResult.Fail<SafaricomKenyaSimSwapResponse>(request, "99");
                
                var headers = new List<KeyValuePair<string, string>>();
                headers.Add(new KeyValuePair<string, string>("Authorization", "Bearer " + token));

                var response = await _apiWrapper.PostAsync(request, endpoint, headers);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    
                    var content = JObject.Parse(response.Content);
                    var lastSwapDate = content.GetValue("lastSwapDate").ToObject<string>();

                    return FeatureResult.Succeed(request, new()
                    {
                        ApiReached = true,
                        IsSuccessful = true,
                        LastSwap = DateTime.ParseExact(lastSwapDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None)
                    });
                }
                //Refresh Token
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await _cacheService.RemoveAsync(SafaricomProviderHelper.SafaricomSwapKey);

                    token = await _safaricomProviderHelper.GetSwapToken();

                    _logger.LogInformation($"Saf Refresh Token {token} {request.CustomerNumber}");

                    if (string.IsNullOrWhiteSpace(token))
                        return FeatureResult.Fail<SafaricomKenyaSimSwapResponse>(request, "99");


                    headers.Add(new KeyValuePair<string, string>("Authorization", "Bearer " + token));
                    response = await _apiWrapper.PostAsync(request, endpoint, headers);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var content = JObject.Parse(response.Content);
                        var lastSwapDate = content.GetValue("lastSwapDate").ToObject<string>();

                        return FeatureResult.Succeed(request, new()
                        {
                            ApiReached = true,
                            IsSuccessful = true,
                            LastSwap = DateTime.ParseExact(lastSwapDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None)
                        });
                    }

                }

                _logger.LogInformation($"SwapCheckUrl Unavailable {response?.StatusCode}");
                return FeatureResult.Fail<SafaricomKenyaSimSwapResponse>(request, response?.StatusCode.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return FeatureResult.Fail<SafaricomKenyaSimSwapResponse>(request, ex.Message);
            }
        }
    }
}