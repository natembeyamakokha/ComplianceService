using RestSharp;

namespace Compliance.Infrastructure.ApiClient
{
    public interface IAPIWrapper<TReq, TRes> : IBaseApiEnabler
         where TRes : new()
         where TReq : class
    {
        Task<RestResponse<TRes>> GetAsync(string url, ICollection<KeyValuePair<string, string>> headers = null);
        Task<RestResponse<TRes>> PostAsync(TReq body, string url, ICollection<KeyValuePair<string, string>> headers = null);
        Task<RestResponse<TRes>> PutAsync(TReq body, string url, ICollection<KeyValuePair<string, string>> headers = null);
        Task<RestResponse> PostRawAsync(TReq body, string url, ICollection<KeyValuePair<string, string>> headers = null);
    }
}