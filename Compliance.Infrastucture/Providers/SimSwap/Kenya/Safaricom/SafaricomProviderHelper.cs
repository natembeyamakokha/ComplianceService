using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Logging;
using Compliance.Infrastructure.ApiClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Distributed;

namespace Compliance.Infrastructure.Providers.Kenya.Safaricom
{
    public class SafaricomProviderHelper
    {
        public static string SafaricomSwapKey = nameof(SafaricomSwapKey);
        private readonly IDistributedCache _cacheService;
        private readonly ILogger<SafaricomProviderHelper> _logger;
        private readonly IConfiguration _configuration;
        private readonly IAPIWrapper<object, object> _apiClient;

        public SafaricomProviderHelper(IDistributedCache cacheService, ILogger<SafaricomProviderHelper> logger, IConfiguration configuration, IAPIWrapper<object, object> apiClient)
        {
            _cacheService = cacheService;
            _logger = logger;
            _configuration = configuration;
            _apiClient = apiClient;
        }

        public async Task<string> GetSwapToken()
        {
            var cachedToken = await _cacheService.GetStringAsync(SafaricomSwapKey);
            if (!string.IsNullOrEmpty(cachedToken))
            {
                _logger.LogInformation($"Cached Token {cachedToken}");
                return cachedToken;
            }

            string endpoint = _configuration["KE:Safaricom:SwapTokenUrl"];
            string username = _configuration["KE:Safaricom:SwapTokenUsername"];
            string password = _configuration["KE:Safaricom:SwapTokenPassword"];
            string token = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes($"{username}:{password}"));

            try
            {
                var headers = new List<KeyValuePair<string, string>>();
                headers.Add(new KeyValuePair<string, string>("Authorization", "Basic " + token));
                
                var response = await _apiClient.GetAsync(endpoint, headers);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = JObject.Parse(response.Content);
                    var access_token = content.GetValue("access_token").ToObject<string>();
                    await _cacheService.SetStringAsync(SafaricomSwapKey, access_token, new DistributedCacheEntryOptions
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
}