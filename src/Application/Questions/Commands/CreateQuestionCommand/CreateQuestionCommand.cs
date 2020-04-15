using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Codidact.Core.Application.Common.Contracts;
using Codidact.Core.Application.Common.Interfaces;
using Codidact.Core.Domain.Common;
using Codidact.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Codidact.Core.Application.Questions.Commands.CreateQuestionCommand
{
    public class CreateQuestionCommand : IRequestHandler<CreateQuestionRequest>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<CreateQuestionCommand> _logger;
        public CreateQuestionCommand(
            IApplicationDbContext context,
            ILogger<CreateQuestionCommand> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<EntityResult> Handle(CreateQuestionRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.UtcNow.ToString("u")} - Starting to handle request for Questions List");

            var category = await _context.Categories
              .FirstOrDefaultAsync(category => category.DisplayName.ToLower() == request.Category.ToLower())
              .ConfigureAwait(false);
            if (category == null)
            {
                throw new Exception($"Category not found: {request.Category}");
            }

            try
            {
                var newQuestion = new Post
                {
                    Body = request.Body,
                    Title = request.Title,
                    PostTypeId = Domain.Enums.PostType.Question,
                    CategoryId = category.Id,
                };
                _context.Posts.Add(newQuestion);

                foreach (var tag in request.Tags)
                {
                    _context.PostTags.Add(new PostTag { TagId = tag, Post = newQuestion });
                }

                await _context.SaveChangesAsync(cancellationToken);
                return new EntityResult(newQuestion.Id);
            }
            catch (Exception e)
            {
                return new EntityResult(e.Message);
            }
        }

    }
}
