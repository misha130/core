using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Codidact.Core.Application.Common.Interfaces;
using Codidact.Core.Application.Questions.Commands.CreateQuestionCommand;
using Codidact.Core.Domain.Entities;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Codidact.Core.Application.IntegrationTests.Questions
{
    public class CreateQuestionCommandTests
    {
        private readonly CreateQuestionCommand _createQuestionCommand;
        private readonly IApplicationDbContext _applicationDbContext;
        public CreateQuestionCommandTests()
        {
            _applicationDbContext = ApplicationDbContextFactory.Create();
            _createQuestionCommand = new CreateQuestionCommand(_applicationDbContext, NullLogger<CreateQuestionCommand>.Instance);
        }

        [Fact]
        public async Task CreatesAQuestion()
        {
            Setup();

            var tagIds = _applicationDbContext.Tags.Select(tag => tag.Id).ToList(); 
            var result = await _createQuestionCommand.Handle(
               new CreateQuestionRequest
               {
                   Title = "A question of time and space",
                   Body = "How do I travel through time and space? <br/> Can't figure it out",
                   Category = "Main",
                   Tags = tagIds
               }, CancellationToken.None);

            Assert.True(result.Success);
            Assert.True(result.Id > 0);

            var createdPost = _applicationDbContext.Posts.Find(result.Id);

            Assert.NotNull(createdPost);
            Assert.NotNull(createdPost.Category);
            Assert.NotNull(createdPost.PostTag);
            Assert.NotEmpty(createdPost.PostTag);
        }

        private void Setup()
        {
            _applicationDbContext.Tags.Add(new Tag { Body = "Time & Space", Description = "Just this concept" });
            _applicationDbContext.SaveChangesAsync(CancellationToken.None);
        }
    }
}
