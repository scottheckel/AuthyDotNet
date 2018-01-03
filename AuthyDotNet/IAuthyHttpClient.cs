using System.Threading.Tasks;
using AuthyDotNet.AuthyHttpClientRequests;
using AuthyDotNet.AuthyHttpClientResponses;

namespace AuthyDotNet
{
    /// <summary>
    /// HTTP Client that can handle requirements of the Authy API
    /// </summary>
    public interface IAuthyHttpClient
    {
        /// <summary>
        /// Get HTTP Method
        /// </summary>
        /// <typeparam name="T">Type of Response</typeparam>
        /// <param name="uri">URI of Resource</param>
        /// <returns>Response</returns>
        Task<T> GetAsync<T>(string uri) where T : AuthyResponse;
        /// <summary>
        /// POST HTTP Method
        /// </summary>
        /// <typeparam name="T">Type of Response</typeparam>
        /// <param name="uri">URI of Resource</param>
        /// <param name="model">Request Model to Post</param>
        /// <returns>Response</returns>
        Task<T> PostAsync<T>(string uri, IAuthyHttpClientRequest model) where T : AuthyResponse;
    }
}
