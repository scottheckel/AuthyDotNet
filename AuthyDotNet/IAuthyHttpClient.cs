using System.Threading.Tasks;
using AuthyDotNet.AuthyHttpClientRequests;
using AuthyDotNet.AuthyHttpClientResponses;

namespace AuthyDotNet
{
    internal interface IAuthyHttpClient
    {
        Task<T> GetAsync<T>(string uri) where T : AuthyResponse;
        Task<T> PostAsync<T>(string uri, IAuthyHttpClientRequest model) where T : AuthyResponse;
    }
}
