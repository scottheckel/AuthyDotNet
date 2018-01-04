using System.Threading.Tasks;
using AuthyDotNet.AuthyHttpClientResponses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NSubstitute;

namespace AuthyDotNet.UnitTests.ApiTests
{
    [TestClass]
    public class RequestSmsTest
    {
        [TestMethod]
        public async Task SuccessNoForceUserDoesntHaveAppTest()
        {
            string responseText = @"{""success"":true,""message"":""SMS token was sent"",""cellphone"":""+1-XXX-XXX-XX12""}";

            var httpClient = Substitute.For<IAuthyHttpClient>();
            httpClient.GetAsync<RequestTokenResponse>("")
                .ReturnsForAnyArgs(JsonConvert.DeserializeObject<RequestTokenResponse>(responseText));
            var api = new Api(httpClient);
            var result = await api.RequestSms("54321");
            Assert.AreEqual("+1-XXX-XXX-XX12", result.Cellphone);
            Assert.AreEqual(null, result.Device);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("SMS token was sent", result.Message);
            Assert.AreEqual(AuthyStatus.Success, result.Status);
        }

        [TestMethod]
        public async Task SuccessForceUserDoesntHaveAppTest()
        {
            string responseText = @"{""success"":true,""message"":""SMS token was sent"",""cellphone"":""+1-XXX-XXX-XX12""}";

            var httpClient = Substitute.For<IAuthyHttpClient>();
            httpClient.GetAsync<RequestTokenResponse>("")
                .ReturnsForAnyArgs(JsonConvert.DeserializeObject<RequestTokenResponse>(responseText));
            var api = new Api(httpClient);
            var result = await api.RequestSms("54321");
            Assert.AreEqual("+1-XXX-XXX-XX12", result.Cellphone);
            Assert.AreEqual(null, result.Device);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("SMS token was sent", result.Message);
            Assert.AreEqual(AuthyStatus.Success, result.Status);
        }

        [TestMethod]
        public async Task SuccessNoForceUserHasAppTest()
        {
            string responseText = @"{""message"":""Ignored: SMS is not needed for smartphones. Pass force=true if you want to actually send it anyway."",""cellphone"":""+1-XXX-XXX-XX12"",""device"":""android"",""ignored"":true,""success"":true}";

            var httpClient = Substitute.For<IAuthyHttpClient>();
            httpClient.GetAsync<RequestTokenResponse>("")
                .ReturnsForAnyArgs(JsonConvert.DeserializeObject<RequestTokenResponse>(responseText));
            var api = new Api(httpClient);
            var result = await api.RequestSms("54321");
            Assert.AreEqual("+1-XXX-XXX-XX12", result.Cellphone);
            Assert.AreEqual("android", result.Device);
            Assert.IsTrue(result.Ignored == true);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Ignored: SMS is not needed for smartphones. Pass force=true if you want to actually send it anyway.", result.Message);
            Assert.AreEqual(AuthyStatus.Success, result.Status);
        }

        [TestMethod]
        public async Task SuccessForceUserHasAppTest()
        {
            string responseText = @"{""success"":true,""message"":""SMS token was sent"",""cellphone"":""+1-XXX-XXX-XX12""}";

            var httpClient = Substitute.For<IAuthyHttpClient>();
            httpClient.GetAsync<RequestTokenResponse>("")
                .ReturnsForAnyArgs(JsonConvert.DeserializeObject<RequestTokenResponse>(responseText));
            var api = new Api(httpClient);
            var result = await api.RequestSms("54321");
            Assert.AreEqual("+1-XXX-XXX-XX12", result.Cellphone);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("SMS token was sent", result.Message);
            Assert.AreEqual(AuthyStatus.Success, result.Status);
        }
    }
}
