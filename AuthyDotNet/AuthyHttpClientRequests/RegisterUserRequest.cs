using System.Collections.Generic;
using System.Net.Http;

namespace AuthyDotNet.AuthyHttpClientRequests
{
    internal class RegisterUserRequest : IAuthyHttpClientRequest
    {
        public int CountryCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool? ShouldSendInstallLinkViaSms { get; set; }

        public HttpContent ToFormContent()
        {
            var parameters = new List<KeyValuePair<string, string>>(new []
            {
                new KeyValuePair<string, string>("user[email]", Email),
                new KeyValuePair<string, string>("user[cellphone]", Phone),
                new KeyValuePair<string, string>("user[country_code]", CountryCode.ToString())

            });
            if (ShouldSendInstallLinkViaSms.HasValue)
            {
                parameters.Add(new KeyValuePair<string, string>("send_install_link_via_sms", ShouldSendInstallLinkViaSms.Value ? "true" : "false"));
            }
            return new FormUrlEncodedContent(parameters);
        }
    }
}
