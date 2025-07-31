using RestSharp;
using RestSharp.Authenticators;
namespace Compliance.Infrastructure.ApiClient
{
    public abstract class BaseApiEnabler : IBaseApiEnabler
    {
        private readonly RestClient _client;
        public BaseApiEnabler()
        {
            _client = new RestClient();
        }

        public void AddBasicAuthentication(string username, string password) =>
            _client.Authenticator = new HttpBasicAuthenticator(username, password);

        public void AddHeader(string name, string value) => _client.AddDefaultHeader(name, value);

        public async Task<RestResponse<T>> ExecuteAsync<T>(RestRequest request) where T : new()
        {
            var response = await _client.ExecuteAsync<T>(request);
            return response;
        }

        public async Task<RestResponse> ExecuteAsync(RestRequest request)
        {
            var response = await _client.ExecuteAsync(request);
            return response;
        }
    }
}