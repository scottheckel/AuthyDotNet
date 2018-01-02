using System.Threading.Tasks;

namespace AuthyDotNet
{
    public interface IAuthyHttpClient
    {
        Task<T> GetAsync<T>(string uri);
        Task<T> PostAsync<T>(string uri, object model);
    }
}
