using Newtonsoft.Json;
using System.Collections.Generic;

namespace AuthyDotNet.AuthyHttpClientResponses
{
    public class VerifyTokenResponse : AuthyResponse
    {
        public string Token { get; set; }
        public DeviceResponse Device { get; set; }
        public Dictionary<string, string> Errors { get; set; }

        public class DeviceResponse
        {
            public string City { get; set; }
            public string Country { get; set; }
            public string Ip { get; set; }
            public string Region { get; set; }
            [JsonProperty("registration_city")]
            public string RegistrationCity { get; set; }
            [JsonProperty("registration_country")]
            public string RegistrationCountry { get; set; }
            [JsonProperty("registration_ip")]
            public string RegistrationIp { get; set; }
            [JsonProperty("registration_method")]
            public string ReigstrationMethod { get; set; }
            [JsonProperty("registration_region")]
            public string RegistartionRegion { get; set; }
            [JsonProperty("os_type")]
            public string OperatingSystemType { get; set; }
            [JsonProperty("last_account_recovery_at")]
            public string LastAccountRecoveryAt { get; set; }
            public int Id { get; set; }
            [JsonProperty("registration_date")]
            public int RegistrationDate { get; set; }
        }
    }
}
