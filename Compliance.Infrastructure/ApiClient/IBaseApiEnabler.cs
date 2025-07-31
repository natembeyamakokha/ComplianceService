using RestSharp;
using System.Threading.Tasks;


namespace Compliance.Infrastructure.ApiClient
{
    public interface IBaseApiEnabler
    {
        void AddBasicAuthentication(string username, string password);
        void AddHeader(string name, string value);
        Task<RestResponse<T>> ExecuteAsync<T>(RestRequest request) where T : new();
    }
}