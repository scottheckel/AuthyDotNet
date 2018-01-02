using Newtonsoft.Json;

namespace AuthyDotNet.AuthyHttpClientResponses
{
    public class UserStatusResponse : AuthyResponse
    {
        public StatusResponse Status { get; set; }

        public class StatusResponse
        {
            [JsonProperty("authy_id")]
            public string AuthyId { get; set; }
            public bool Confirmed { get; set; }
            public bool Registered { get; set; }
            [JsonProperty("country_code")]
            public int CountryCode { get; set; }
            [JsonProperty("phone_number")]
            public string PhoneNumber { get; set; }
            public string[] Devices { get; set; }
        }
    }
}
