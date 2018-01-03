using System.Net.Http;

namespace AuthyDotNet.AuthyHttpClientRequests
{
    public interface IAuthyHttpClientRequest
    {
        HttpContent ToFormContent();
    }
}
