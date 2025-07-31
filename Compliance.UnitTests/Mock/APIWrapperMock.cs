using Compliance.Infrastructure.ApiClient;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Compliance.UnitTests.Mock;

public class APIWrapperMock<TReq, TRes> : BaseApiEnabler, IAPIWrapper<TReq, TRes>
 where TReq : class
 where TRes : new()
{
    public async Task<RestResponse<TRes>> GetAsync(string url, ICollection<KeyValuePair<string, string>> headers = null)
    {
        // Simulate a mock response for GetAsync
        return await Task.FromResult(new RestResponse<TRes>
        {
            StatusCode = System.Net.HttpStatusCode.OK
        });
    }

    public async Task<RestResponse<TRes>> PostAsync(TReq body, string url, ICollection<KeyValuePair<string, string>> headers = null)
    {
        // Simulate a mock response for PostAsync
        return await Task.FromResult(new RestResponse<TRes>
        {
            IsSuccessful = true,
            StatusCode = System.Net.HttpStatusCode.Created
        });
    }

    public async Task<RestResponse<TRes>> PutAsync(TReq body, string url, ICollection<KeyValuePair<string, string>> headers = null)
    {
        // Simulate a mock response for PutAsync
        return await Task.FromResult(new RestResponse<TRes>
        {
            StatusCode = System.Net.HttpStatusCode.OK
        });
    }

    public async Task<RestResponse> PostRawAsync(TReq body, string url, ICollection<KeyValuePair<string, string>> headers = null)
    {
        // Simulate a mock response for PostRawAsync
        return await Task.FromResult(new RestResponse
        {
            StatusCode = System.Net.HttpStatusCode.Created
        });
    }
}
