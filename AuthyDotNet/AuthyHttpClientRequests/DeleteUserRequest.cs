using System.Collections.Generic;
using System.Net.Http;

namespace AuthyDotNet.AuthyHttpClientRequests
{
    internal class DeleteUserRequest : IAuthyHttpClientRequest
    {
        public string UserIp { get; set; }

        public HttpContent ToFormContent()
        {
            return new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("user_ip", UserIp) 
            });
        }
    }
}
