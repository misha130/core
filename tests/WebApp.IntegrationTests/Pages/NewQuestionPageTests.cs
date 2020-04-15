using System.Net;
using System.Threading.Tasks;
using Codidact.Core.Application.Questions.Commands.CreateQuestionCommand;
using Xunit;


namespace Codidact.Core.WebApp.IntegrationTests.Pages
{
    public class NewQuestionPageTests :
      IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public NewQuestionPageTests(
            CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("main/questions/new")]
        public async Task ReturnsQuestionsByQueryParams(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html",
                response.Content.Headers.ContentType.MediaType);
        }

        [Theory]
        [InlineData("wrong-category/questions/new")]
        public async Task IncorrectCategoryThrowsServerError(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task CreatingAQuestionRedirectsToIt()
        {

            // Arrange
            var client = _factory.CreateClient();
                
            var defaultPage = await client.GetAsync("main/questions/new");

            var content = IntegrationTestHelper.GetRequestContent(new CreateQuestionRequest
            {
                Body = "Hello",
                Title = "Some Title",
                Category = "Main",
            });

            // Act
            var response = await client.PostAsync("main/questions/new", content);

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }
    }
}
