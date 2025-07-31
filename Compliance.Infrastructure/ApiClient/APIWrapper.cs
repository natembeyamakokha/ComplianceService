using RestSharp;


namespace Compliance.Infrastructure.ApiClient
{
    public class APIWrapper<TReq, TRes> : BaseApiEnabler, IAPIWrapper<TReq, TRes>
    where TReq : class
    where TRes : new()
    {

        public async Task<RestResponse<TRes>> GetAsync(string url, ICollection<KeyValuePair<string, string>> headers = null)
        {
            var request = new RestRequest(url, Method.Get);
            if (headers is not null)
                request.AddHeaders(headers);
            return await ExecuteAsync<TRes>(request);
        }

        public async Task<RestResponse<TRes>> PostAsync(TReq body, string url, ICollection<KeyValuePair<string, string>> headers = null)
        {
            var request = new RestRequest(url, Method.Post);
            if (headers is not null)
                request.AddHeaders(headers);
            request.AddJsonBody(body);
            return await ExecuteAsync<TRes>(request);
        }

        public async Task<RestResponse<TRes>> PutAsync(TReq body, string url, ICollection<KeyValuePair<string, string>> headers = null)
        {
            var request = new RestRequest(url, Method.Put);
            if (headers is not null)
                request.AddHeaders(headers);
            request.AddJsonBody(body);
            return await ExecuteAsync<TRes>(request);
        }

        public async Task<RestResponse> PostRawAsync(TReq body, string url, ICollection<KeyValuePair<string, string>> headers = null)
        {
            var request = new RestRequest(url, Method.Post);
            if (headers is not null)
                request.AddHeaders(headers);
            request.AddJsonBody(body);
            return await ExecuteAsync(request);
        }

    }
}