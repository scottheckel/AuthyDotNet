using AuthyDotNet.AuthyHttpClientRequests;
using AuthyDotNet.AuthyHttpClientResponses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthyDotNet
{
    public class Api
    {
        private IAuthyHttpClient client;

        public Api(string apiKey, string apiUrl = "https://api.authy.com")
        {
            client = new AuthyHttpClient(apiKey, apiUrl);
        }

        internal Api(IAuthyHttpClient httpClient)
        {
            client = httpClient;
        }

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

        public async Task<VerifyTokenResponse> VerifyToken(string authyId, string token, bool force = true)
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
            string uri = $"sms/{authyId}" + (force ? "?force=true" : "");
            return await client.GetAsync<VerifyTokenResponse>(uri);
        }

        public async Task<RequestTokenResponse> RequestSms(string authyId, bool force = false)
        {
            authyId = Helpers.SanitizeNumber(authyId);
            string uri = $"sms/{authyId}" + (force ? "?force=true" : "");
            return await client.GetAsync<RequestTokenResponse>(uri);
        }

        public async Task<RequestTokenResponse> RequestPhoneCall(string authyId, bool force = false)
        {
            authyId = Helpers.SanitizeNumber(authyId);
            string uri = $"call/{authyId}" + (force ? "?force=true" : "");
            return await client.GetAsync<RequestTokenResponse>(uri);
        }

        public async Task<AuthyResponse> DeleteUser(string authyId, string userIp = null)
        {
            authyId = Helpers.SanitizeNumber(authyId);
            string uri = $"users/delete/{authyId}";
            return await client.PostAsync<AuthyResponse>(uri, new DeleteUserRequest { UserIp = userIp });
        }

        public async Task<UserStatusResponse> UserStatus(string authyId)
        {
            authyId = Helpers.SanitizeNumber(authyId);
            string uri = $"users/{authyId}/status";
            return await client.GetAsync<UserStatusResponse>(uri);
        }
    }
}
