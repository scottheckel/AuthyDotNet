using AuthyDotNet.AuthyHttpClientRequests;
using AuthyDotNet.AuthyHttpClientResponses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthyDotNet
{
    /// <summary>
    /// Authy API Endpoints
    /// </summary>
    public class Api
    {
        private IAuthyHttpClient client;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiKey">API Key</param>
        /// <param name="apiUrl">API Url</param>
        public Api(string apiKey, string apiUrl = "https://api.authy.com")
        {
            client = new AuthyHttpClient(apiKey, apiUrl);
        }

        /// <summary>
        /// Internal Constructor
        /// </summary>
        /// <param name="httpClient">Http Client to Use</param>
        internal Api(IAuthyHttpClient httpClient)
        {
            client = httpClient;
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="cellphone">Cellphone</param>
        /// <param name="countryCode">(Optional) Country Code (Default: 1)</param>
        /// <returns></returns>
        public async Task<RegisterUserResponse> RegisterUser(string email, string cellphone, int countryCode = 1)
        {
            string uri = $"users/new";
            return await client.PostAsync<RegisterUserResponse>(uri, new RegisterUserRequest
            {
                CountryCode = countryCode, 
                Email = email,
                Phone = cellphone,
                ShouldSendInstallLinkViaSms = false
            });
        }

        /// <summary>
        /// Verify Token
        /// </summary>
        /// <param name="authyId">Authy User Id</param>
        /// <param name="token">Token</param>
        /// <returns>Authy Response</returns>
        public async Task<VerifyTokenResponse> VerifyToken(string authyId, string token)
        {
            if (!Helpers.TokenIsValid(token))
            {
                return new VerifyTokenResponse
                {
                    Message = "is invalid",
                    Success = false,
                    Errors = new Dictionary<string, string> {
                        { "token", "is invalid" }
                    }
                };
            }

            authyId = Helpers.SanitizeNumber(authyId);
            token = Helpers.SanitizeNumber(token);
            string uri = $"verify/{token}/{authyId}";
            return await client.GetAsync<VerifyTokenResponse>(uri);
        }

        /// <summary>
        /// Request Token via Authy/SMS
        /// </summary>
        /// <param name="authyId">Authy User Id</param>
        /// <param name="forceSms">(Optional) Force SMS if user has Authy installed (Default: false)</param>
        /// <returns>Authy Response</returns>
        public async Task<RequestTokenResponse> RequestSms(string authyId, bool forceSms = false)
        {
            authyId = Helpers.SanitizeNumber(authyId);
            string uri = $"sms/{authyId}" + (forceSms ? "?force=true" : "");
            return await client.GetAsync<RequestTokenResponse>(uri);
        }

        /// <summary>
        /// Request Token via Authy/Phone Call
        /// </summary>
        /// <param name="authyId">Authy User Id</param>
        /// <param name="forcePhone">(Optional) Force Phone Call if user has Authy installed (Default: false)</param>
        /// <returns>Authy Response</returns>
        public async Task<RequestTokenResponse> RequestPhoneCall(string authyId, bool forcePhone = false)
        {
            authyId = Helpers.SanitizeNumber(authyId);
            string uri = $"call/{authyId}" + (forcePhone ? "?force=true" : "");
            return await client.GetAsync<RequestTokenResponse>(uri);
        }

        /// <summary>
        /// Delete User from App
        /// </summary>
        /// <param name="authyId">Authy User Id</param>
        /// <param name="userIp">(Optional) IP of requesting User</param>
        /// <returns>Authy Response</returns>
        public async Task<AuthyResponse> DeleteUser(string authyId, string userIp = null)
        {
            authyId = Helpers.SanitizeNumber(authyId);
            string uri = $"users/delete/{authyId}";
            return await client.PostAsync<AuthyResponse>(uri, new DeleteUserRequest { UserIp = userIp });
        }

        /// <summary>
        /// User Status from App
        /// </summary>
        /// <param name="authyId">Authy User Id</param>
        /// <returns>Authy Response</returns>
        public async Task<UserStatusResponse> UserStatus(string authyId)
        {
            authyId = Helpers.SanitizeNumber(authyId);
            string uri = $"users/{authyId}/status";
            return await client.GetAsync<UserStatusResponse>(uri);
        }
    }
}
