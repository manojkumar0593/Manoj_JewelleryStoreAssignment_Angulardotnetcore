using AspNetCore.JwtAuthentication.PasswordHasing.Plugin;
using FluentAssertions;
using JewelleryStoreAPI.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JewelleryStore.IntegrationTest
{
    public class UserAPIIntegrationTest
    {
        [Fact]
        public async Task TestLoginSuccessStatus()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/v1/User/Login", new StringContent(
                    JsonConvert.SerializeObject(new UserViewModel { EmailId = "admin", Password = "admin123" })
                    , Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                //Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }

        [Fact]
        public async Task TestLoginFailedStatus()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/v1/User/Login", new StringContent(
                    JsonConvert.SerializeObject(new UserViewModel { EmailId = "admin", Password = "admin" })
                    , Encoding.UTF8, "application/json"));

                //Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
                response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            }
        }

        [Fact]
        public async Task TestLoginVerifyRole()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/v1/User/Login", new StringContent(
                    JsonConvert.SerializeObject(new UserViewModel { EmailId = "admin", Password = "admin123" })
                    , Encoding.UTF8, "application/json"));
                var data = JsonConvert.DeserializeObject<UserViewModel>(await response.Content.ReadAsStringAsync());
                response.EnsureSuccessStatusCode();
                //Assert.Equal("Privileged", data.Role);
                data.Role.Should().Be("Privileged");
            }
        }

        [Fact]
        public async Task TestLoginVerifyIncorrectRole()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/v1/User/Login", new StringContent(
                    JsonConvert.SerializeObject(new UserViewModel { EmailId = "admin", Password = "admin123" })
                    , Encoding.UTF8, "application/json"));
                var data = JsonConvert.DeserializeObject<UserViewModel>(await response.Content.ReadAsStringAsync());
                response.EnsureSuccessStatusCode();
                //Assert.Equal("Privileged", data.Role);
                data.Role.Should().NotBe("Regular");
            }
        }

        [Theory]
        [InlineData("gold")]
        [InlineData("silver")]
        public async Task TestGetPrice(string jewel)
        {
            using (var client = new TestClientProvider().Client)
            {
                ITokenService _tokenService = new TokenService();

                var usersClaims = new[]
                 {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.NameIdentifier, "3")
                };
                var token = _tokenService.GenerateAccessToken(
                    usersClaims,
                    "localhost.com",
                    "localhost.com",
                    "ASPNETCORESECRETKEYFORAUTHENTICATIONANDAUTHORIZATION",
                    "60"
                );
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync("/api/v1/Jewellery/GetJewelPrice?jewel="+jewel);
                
                response.EnsureSuccessStatusCode();
                //Assert.Equal("Privileged", data.Role);
                response.Should().NotBeNull();
            }
        }
    }
}
