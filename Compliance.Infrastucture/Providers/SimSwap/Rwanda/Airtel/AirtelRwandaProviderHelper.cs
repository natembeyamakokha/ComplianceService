using System.Net;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Logging;
using Compliance.Infrastructure.ApiClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Distributed;

namespace Compliance.Infrastructure.Providers.Rwanda.Airtel
{
    public class AirtelRwandaProviderHelper
    {
        public static string AirtelRwandaSwapKey = nameof(AirtelRwandaSwapKey);

        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _cacheService;
        private readonly IAPIWrapper<object, object> _apiClient;
        private readonly ILogger<AirtelRwandaProviderHelper> _logger;

        public AirtelRwandaProviderHelper(IDistributedCache cacheService, ILogger<AirtelRwandaProviderHelper> logger, IConfiguration configuration, IAPIWrapper<object, object> apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
            _cacheService = cacheService;
            _configuration = configuration;
        }

        public async Task<string> GetSwapToken()
        {
            var cachedToken = await _cacheService.GetStringAsync(AirtelRwandaSwapKey);
            if (!string.IsNullOrEmpty(cachedToken))
            {
                _logger.LogInformation($"Cached Token {cachedToken}");
                return cachedToken;
            }
            

            string endpoint = _configuration["RW:Airtel:TokenUrl"];
            string clientId = _configuration["RW:Airtel:ClientId"];
            string clientSecret = _configuration["RW:Airtel:ClientSecret"];

            var payload = new
            {
                client_id = clientId,
                client_secret = clientSecret,
                grant_type = "client_credentials"
            };

            try
            {

                var response = await _apiClient.PostAsync(payload, endpoint);
                
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = JObject.Parse(response.Content);
                    var access_token = content.GetValue("access_token").ToObject<string>();
                    await _cacheService.SetStringAsync(AirtelRwandaSwapKey, access_token, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                    });
                    _logger.LogInformation($"Access Token {access_token}");
                    return access_token;
                }
                _logger.LogInformation($"SwapTokenUrl Unavailable {response.StatusCode}");
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return string.Empty;
            }
        }
    }

    public static class Extensions
    {
        public static string GetLast(this string source, int numberOfChars)
        {
            if (numberOfChars >= source.Length)
                return source;
            return source.Substring(source.Length - numberOfChars);
        }
    }
}