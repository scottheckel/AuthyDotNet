using System.Threading.Tasks;
using AuthyDotNet.AuthyHttpClientResponses;

namespace AuthyDotNet
{
    internal interface IAuthyHttpClient
    {
        Task<T> GetAsync<T>(string uri) where T : AuthyResponse;
        Task<T> PostAsync<T>(string uri, object model) where T : AuthyResponse;
    }
}
