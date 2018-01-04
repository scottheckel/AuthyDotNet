using System;
using System.Threading.Tasks;
using AuthyDotNet.AuthyHttpClientResponses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NSubstitute;

namespace AuthyDotNet.UnitTests.ApiTests
{
    [TestClass]
    public class UserStatusTest
    {
        [TestMethod]
        public async Task SuccessUserDoesntHaveAppTest()
        {
            string responseText = @"{""status"":{""authy_id"":54321,""confirmed"":false,""registered"":false,""country_code"":1,""phone_number"":""XXX-XXX-1234"",""devices"":[],""has_hard_token"":false,""account_disabled"":false,""detailed_devices"":[{""device_type"":""authy"",""os_type"":""unknown"",""creation_date"":1515034168}]},""message"":""User status."",""success"":true}";

            var httpClient = Substitute.For<IAuthyHttpClient>();
            httpClient.GetAsync<UserStatusResponse>("")
                .ReturnsForAnyArgs(JsonConvert.DeserializeObject<UserStatusResponse>(responseText));
            var api = new Api(httpClient);
            var result = await api.UserStatus("54321");
            Assert.AreEqual("54321", result.UserStatus.AuthyId);
            Assert.AreEqual(false, result.UserStatus.Confirmed);
            Assert.AreEqual("XXX-XXX-1234", result.UserStatus.PhoneNumber);
            Assert.AreEqual(0, result.UserStatus.Devices.Length);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("User status.", result.Message);
            Assert.AreEqual(AuthyStatus.Success, result.Status);
        }

        [TestMethod]
        public async Task SuccessUserHasAppTest()
        {
            string responseText = @"{""status"":{""authy_id"":54321,""confirmed"":true,""registered"":true,""country_code"":1,""phone_number"":""XXX-XXX-1234"",""devices"":[""android""],""has_hard_token"":false,""account_disabled"":false,""detailed_devices"":[{""device_type"":""authy"",""os_type"":""android"",""creation_date"":1487914821}]},""message"":""User status."",""success"":true}";

            var httpClient = Substitute.For<IAuthyHttpClient>();
            httpClient.GetAsync<UserStatusResponse>("")
                .ReturnsForAnyArgs(JsonConvert.DeserializeObject<UserStatusResponse>(responseText));
            var api = new Api(httpClient);
            var result = await api.UserStatus("54321");
            Assert.AreEqual("54321", result.UserStatus.AuthyId);
            Assert.AreEqual(true, result.UserStatus.Confirmed);
            Assert.AreEqual("XXX-XXX-1234", result.UserStatus.PhoneNumber);
            Assert.AreEqual(1, result.UserStatus.Devices.Length);
            Assert.AreEqual("android", result.UserStatus.Devices[0]);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("User status.", result.Message);
            Assert.AreEqual(AuthyStatus.Success, result.Status);
        }
    }
}
