using System.Net.Http;

namespace AuthyDotNet.AuthyHttpClientRequests
{
    interface IAuthyHttpClientRequest
    {
        HttpContent ToFormContent();
    }
}
