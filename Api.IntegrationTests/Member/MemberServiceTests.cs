using Moq;
using Xunit;
using Codidact.Core.Api;
using Grpc.Net.Client;
using Codidact.Core.WebApp.IntegrationTests;
using Grpc.Core;

namespace Api.IntegrationTests.Members
{
    public class MemberServiceTests :
      IClassFixture<GrpcApiApplicationFactory<Startup>>
    {
        private readonly GrpcApiApplicationFactory<Startup> _factory;

        public MemberServiceTests(
            GrpcApiApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public void CreateMemberWithClient()
        {
            var client = _factory.CreateClient();
            var options = new GrpcChannelOptions()
            {
                HttpClient = client
            };
            var channel = GrpcChannel.ForAddress("http://localhost", options);
            var memberClient = new Member.MemberClient(channel);

            var reply = memberClient.CreateMember(
                                new MemberCreationRequest
                                {
                                    DisplayName = "GreeterClient",
                                    UserId = 1
                                });
            Assert.True(reply.Success);
        }
    }
}
