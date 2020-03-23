using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Codidact.Core.Api;
using Codidact.Core.WebApp.IntegrationTests;
using Grpc.Net.Client;
using Xunit;

namespace Codidact.Core.Application.IntegrationTests.Auths
{
    public class AuthServiceTests :
       IClassFixture<GrpcApiApplicationFactory<Startup>>
    {
        private readonly GrpcApiApplicationFactory<Startup> _factory;

        public AuthServiceTests(
            GrpcApiApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetClientAuth()
        {
            var client = _factory.CreateClient();
            var options = new GrpcChannelOptions()
            {
                HttpClient = client
            };
            var channel = GrpcChannel.ForAddress("http://localhost", options);
            var authClient = new Auth.AuthClient(channel);

            var reply = await authClient.AuthorizeClientAsync(
                                new ClientAuthRequest
                                {
                                    ClientId = Guid.NewGuid().ToString(),
                                    ClientSecret = Guid.NewGuid().ToString()
                                });
            Assert.NotNull(reply.Token);
        }
    }
}
