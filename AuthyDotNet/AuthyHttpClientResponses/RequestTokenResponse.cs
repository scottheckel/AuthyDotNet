namespace AuthyDotNet.AuthyHttpClientResponses
{
    public class RequestTokenResponse : AuthyResponse
    {
        public string Cellphone { get; set; }
        public string Device { get; set; }
        public bool? Ignored { get; set; }
    }
}
