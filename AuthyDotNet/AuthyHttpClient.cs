using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AuthyDotNet.AuthyHttpClientRequests;
using AuthyDotNet.AuthyHttpClientResponses;

namespace AuthyDotNet
{
    /// <summary>
    /// HTTP Client that can handle requirements of the Authy API
    /// </summary>
    internal class AuthyHttpClient : IAuthyHttpClient
    {
        private readonly string apiKey;
        private readonly string baseUrl;

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
        public async Task<T> GetAsync<T>(string uri) where T : AuthyResponse
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
        public async Task<T> PostAsync<T>(string uri, IAuthyHttpClientRequest model) where T : AuthyResponse
        {
            return await SendAsync<T>(baseUrl + uri, HttpMethod.Post, model);
        }

        private async Task<T> SendAsync<T>(string url, HttpMethod method, IAuthyHttpClientRequest model = null) where T : AuthyResponse
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url),
                    Method = method
                };
                request.Headers.Add("X-Authy-API-Key", apiKey);
                request.Headers.Add("User-Agent", GetUserAgent());

                if (model != null)
                {
                    request.Content = model.ToFormContent();
                }

                var httpResponse = await httpClient.SendAsync(request);
                var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                var authyResponse = JsonConvert.DeserializeObject<T>(httpResponseBody);
                switch (httpResponse.StatusCode) // TODO: SH - Redo this for all status code not just those handled by Authy.NET
                {
                    case HttpStatusCode.ServiceUnavailable:
                        authyResponse.Status = AuthyStatus.ServiceUnavailable;
                        break;
                    case HttpStatusCode.Unauthorized:
                        authyResponse.Status = AuthyStatus.Unauthorized;
                        break;
                    case HttpStatusCode.BadRequest:
                        authyResponse.Status = AuthyStatus.BadRequest;
                        break;
                }
                return authyResponse;
            }
        }

        private string GetUserAgent()
        {
            return $"AuthyDotNet/0.2.2";
        }
    }
}
