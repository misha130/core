using Codidact.Auth;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Auth.IntegratonTests
{
    public class AuthRequestTests :
    IClassFixture<AuthWebApplicationFactory<Startup>>
    {
        private readonly AuthWebApplicationFactory<Startup> _factory;

        public AuthRequestTests(
            AuthWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task AuthRetrievesToken()
        {
            var client = _factory.CreateClient();
            var tokenRequestData = "grant_type=password&username=johndoe&password=password";
            StringContent tokenRequestContent = new StringContent(tokenRequestData,
                Encoding.UTF8,
                "application/x-www-form-urlencoded");

            var result = await client.PostAsync("/token", tokenRequestContent);
            result.EnsureSuccessStatusCode();

        }
    }
}
