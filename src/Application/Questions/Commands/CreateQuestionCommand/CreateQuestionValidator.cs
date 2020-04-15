using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace Codidact.Core.Application.Questions.Commands.CreateQuestionCommand
{
    public class CreateQuestionValidator : AbstractValidator<CreateQuestionRequest>
    {
        public CreateQuestionValidator()
        {
            RuleFor(request => request.Title).NotNull();

            RuleFor(request => request.Body).NotNull();

            RuleFor(request => request.Category)
                       .NotNull()
                       .NotEmpty();

            //RuleFor(request => request.Tags).NotEmpty();

            //RuleForEach(request => request.Tags).Must(tagId => tagId > 0);
        }
    }

}
