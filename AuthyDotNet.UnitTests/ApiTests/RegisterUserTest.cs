using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using AuthyDotNet.AuthyHttpClientResponses;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AuthyDotNet.UnitTests.ApiTests
{
    [TestClass]
    public class RegisterUserTest
    {
        [TestMethod]
        public async Task SuccessTest()
        {
            string responseText = @"
                    {
                        ""message"":""User created successfully."",
                        ""user"": {
                            ""id"":2
                        },
                        ""success"":true
                    }
                ";

            var httpClient = Substitute.For<IAuthyHttpClient>();
            httpClient.PostAsync<RegisterUserResponse>("", null)
                .ReturnsForAnyArgs(JsonConvert.DeserializeObject<RegisterUserResponse>(responseText));
            var api = new Api(httpClient);
            var result = await api.RegisterUser("user@domain.com", "3173389302", 54);
            Assert.AreEqual(2, result.UserId);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("User created successfully.", result.Message);
            Assert.AreEqual(AuthyStatus.Success, result.Status);
        }
        
    }
}
