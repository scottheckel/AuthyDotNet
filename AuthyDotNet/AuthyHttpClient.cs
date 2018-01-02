using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AuthyDotNet
{
    /// <summary>
    /// HTTP Client that can handle requirements of the Authy API
    /// </summary>
    public class AuthyHttpClient : IAuthyHttpClient
    {
        private string apiKey;
        private string baseUrl;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiKey">API Key</param>
        /// <param name="apiUrl">(Optional) API URL</param>
        public AuthyHttpClient(string apiKey, string apiUrl = "https://api.authy.com")
        {
            this.apiKey = apiKey;
            this.baseUrl = apiUrl + "/protected/json/"; // NOTE: We always do JSON never XML
        }

        /// <summary>
        /// Get HTTP Method
        /// </summary>
        /// <typeparam name="T">Type of Response</typeparam>
        /// <param name="uri">URI of Resource</param>
        /// <returns>Response</returns>
        public async Task<T> GetAsync<T>(string uri)
        {
            return await SendAsync<T>(baseUrl + uri, HttpMethod.Get);
        }

        /// <summary>
        /// POST HTTP Method
        /// </summary>
        /// <typeparam name="T">Type of Response</typeparam>
        /// <param name="uri">URI of Resource</param>
        /// <param name="model">Request Model to Post</param>
        /// <returns>Response</returns>
        public async Task<T> PostAsync<T>(string uri, object model)
        {
            return await SendAsync<T>(baseUrl + uri, HttpMethod.Post);
        }

        private async Task<T> SendAsync<T>(string url, HttpMethod method, object model = null)
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Get
                };
                request.Headers.Add("X-Authy-API-Key", apiKey);
                request.Headers.Add("User-Agent", GetUserAgent());

                if (model != null)
                {
                    request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8);
                }

                var httpResponse = await httpClient.SendAsync(request);
                var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                var authyResponse = JsonConvert.DeserializeObject<T>(httpResponseBody);
                switch (httpResponse.StatusCode)
                {
                    case HttpStatusCode.ServiceUnavailable:

                        break;
                    case HttpStatusCode.Unauthorized:

                        break;
                    case HttpStatusCode.BadRequest:

                        break;
                }
                return authyResponse;
            }
        }

        private string GetUserAgent()
        {
            return $"AuthyDotNet/0.1.0";
        }
    }
}
