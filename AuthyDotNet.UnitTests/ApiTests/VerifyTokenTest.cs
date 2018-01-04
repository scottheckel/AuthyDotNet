using System.Threading.Tasks;
using AuthyDotNet.AuthyHttpClientResponses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NSubstitute;

namespace AuthyDotNet.UnitTests.ApiTests
{
    [TestClass]
    public class VerifyTokenTest
    {
        [TestMethod]
        public async Task SuccessUserDoesntHaveAppTest()
        {
            string responseText = @"{""message"":""Token is valid."",""token"":""is valid"",""success"":""true"",""device"":{""id"":null,""os_type"":""sms"",""registration_date"":1515034168,""registration_method"":null,""registration_country"":null,""registration_region"":null,""registration_city"":null,""country"":null,""region"":null,""city"":null,""ip"":null,""last_account_recovery_at"":null,""last_sync_date"":null}}";

            var httpClient = Substitute.For<IAuthyHttpClient>();
            httpClient.GetAsync<VerifyTokenResponse>("")
                .ReturnsForAnyArgs(JsonConvert.DeserializeObject<VerifyTokenResponse>(responseText));
            var api = new Api(httpClient);
            var result = await api.VerifyToken("54321", "123456");
            Assert.AreEqual("sms", result.Device.OperatingSystemType);
            Assert.AreEqual("is valid", result.Token);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Token is valid.", result.Message);
            Assert.AreEqual(AuthyStatus.Success, result.Status);
        }

        [TestMethod]
        public async Task InvalidShortTokenTest()
        {
            var httpClient = Substitute.For<IAuthyHttpClient>();
            var api = new Api(httpClient);
            var result = await api.VerifyToken("54321", "12345");
            Assert.IsNull(result.Device);
            Assert.AreEqual("is invalid", result.Token);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Token is invalid.", result.Message);
            Assert.AreEqual(AuthyStatus.Success, result.Status);
        }

        [TestMethod]
        public async Task InvalidLongTokenTest()
        {
            var httpClient = Substitute.For<IAuthyHttpClient>();
            var api = new Api(httpClient);
            var result = await api.VerifyToken("54321", "12345678901");
            Assert.IsNull(result.Device);
            Assert.AreEqual("is invalid", result.Token);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Token is invalid.", result.Message);
            Assert.AreEqual(AuthyStatus.Success, result.Status);
        }
    }
}
