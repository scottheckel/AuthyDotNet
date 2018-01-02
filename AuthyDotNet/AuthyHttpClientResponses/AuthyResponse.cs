namespace AuthyDotNet.AuthyHttpClientResponses
{
    public class AuthyResponse
    {
        public string Message { get; set; }
        public AuthyStatus Status { get; set; }
        public bool Success { get; set; }
    }
}
