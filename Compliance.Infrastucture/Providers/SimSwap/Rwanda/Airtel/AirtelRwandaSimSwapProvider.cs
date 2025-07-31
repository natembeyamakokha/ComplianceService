using System.Net;
using Omni.Features;
using Omni.Providers;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Logging;
using Compliance.Infrastructure.ApiClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Distributed;
using Compliance.Infrastructure.Providers.Rwanda.Airtel;

namespace Compliance.Infrastructure.Providers.Rwanda.Mtn
{
    internal sealed class AirtelRwandaSimSwapProvider : FeatureProvider<AirtelRwandaSimSwapRequest, AirtelRwandaSimSwapResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _cacheService;
        private readonly ILogger<AirtelRwandaSimSwapProvider> _logger;
        private readonly AirtelRwandaProviderHelper _airtelRwandaProviderHelper;
        private readonly IAPIWrapper<AirtelRwandaSimSwapRequest, object> _apiWrapper;

        public AirtelRwandaSimSwapProvider(IConfiguration configuration, AirtelRwandaProviderHelper airtelRwandaProviderHelper, ILogger<AirtelRwandaSimSwapProvider> logger, IAPIWrapper<AirtelRwandaSimSwapRequest, object> apiWrapper, IDistributedCache cacheService)
        {
            _logger = logger;
            _apiWrapper = apiWrapper;
            _cacheService = cacheService;
            _configuration = configuration;
            _airtelRwandaProviderHelper = airtelRwandaProviderHelper;
        }

        protected override async Task<FeatureResult<AirtelRwandaSimSwapResponse>> ExecuteAsync(AirtelRwandaSimSwapRequest request, IFeatureContext context)
        {

            try
            {
                string url = _configuration["RW:Airtel:Url"];
                string Cookie = _configuration["RW:Airtel:Cookie"];
                string ASP_OPCO = _configuration["RW:Airtel:ASPOPCO"];
                string ASPLocale = _configuration["RW:Airtel:ASPLocale"];
                string transactionId = _configuration["RW:Airtel:AspConsumerTxnId"];

                var headers = new List<KeyValuePair<string, string>>();
                headers.Add(new KeyValuePair<string, string>("ASP-Consumer-Txn-Id", $"{transactionId}"));
                headers.Add(new KeyValuePair<string, string>("ASP-Locale", $"{ASPLocale}"));
                headers.Add(new KeyValuePair<string, string>("ASP-OPCO", $"{ASP_OPCO}"));
                headers.Add(new KeyValuePair<string, string>("ASP-Req-Timestamp", $"{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}"));
                headers.Add(new KeyValuePair<string, string>("Cookie", $"{Cookie}"));

                var token = await _airtelRwandaProviderHelper.GetSwapToken();

                _logger.LogInformation($"airtel Cached Token {token} {request.msisdn}");

                if (string.IsNullOrWhiteSpace(token))
                    return FeatureResult.Fail<AirtelRwandaSimSwapResponse>(request, "99");

                headers.Add(new KeyValuePair<string, string>("Authorization", "Bearer " + token));

                var response = await _apiWrapper.PostAsync(request, url, headers);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = JObject.Parse(response.Content);
                    var parsedContent = content.ToObject<AirtelRwandaSimSwapResponse>();
                    if (parsedContent?.Response?.IsSimSwapped == true)
                    {
                        return FeatureResult.Succeed(request, new()
                        {
                            ApiReached = true,
                            IsSuccessful = true,
                            LastSwap = DateTime.Parse(parsedContent.Response.DateTime)
                        });
                    }
                    else if (parsedContent?.Response?.IsSimSwapped == false)
                    {
                        return FeatureResult.Succeed(request, new()
                        {
                            IsSuccessful = false,
                            LastSwap = new DateTime()
                        });
                    }
                    else
                    {
                        return FeatureResult.Succeed(request, new()
                        {
                            IsSuccessful = false,
                            LastSwap = new DateTime()
                        });
                    }

                }
                //Refresh Token
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await _cacheService.RemoveAsync(AirtelRwandaProviderHelper.AirtelRwandaSwapKey);

                    token = await _airtelRwandaProviderHelper.GetSwapToken();

                    if (string.IsNullOrWhiteSpace(token))
                        return FeatureResult.Fail<AirtelRwandaSimSwapResponse>(request, "99");

                    headers.Add(new KeyValuePair<string, string>("Authorization", "Bearer " + token));

                    response = await _apiWrapper.PostAsync(request, url, headers);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var content = JObject.Parse(response.Content);
                        var parsedContent = content.ToObject<AirtelRwandaSimSwapResponse>();
                        if (parsedContent?.Response?.IsSimSwapped == true)
                        {
                            return FeatureResult.Succeed(request, new()
                            {
                                IsSuccessful = true,
                                LastSwap = DateTime.Parse(parsedContent.Response.DateTime)
                            });
                        }
                        else if (parsedContent?.Response?.IsSimSwapped == false)
                        {
                            return FeatureResult.Succeed(request, new()
                            {
                                IsSuccessful = false,
                                LastSwap = new DateTime()
                            });
                        }
                        else
                        {
                            return FeatureResult.Succeed(request, new()
                            {
                                IsSuccessful = false,
                                LastSwap = new DateTime()
                            });
                        }
                    }
                }

                return FeatureResult.Fail<AirtelRwandaSimSwapResponse>(request, response?.StatusCode.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return FeatureResult.Fail<AirtelRwandaSimSwapResponse>(request, ex.Message);
            }
        }
    }
}