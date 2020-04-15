using System;
using System.Threading.Tasks;
using Codidact.Core.Application.Questions.Commands.CreateQuestionCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Codidact.Core.WebApp.Pages.Posts
{
    [Authorize]
    public class NewQuestionModel : PageModel
    {
        private readonly CreateQuestionCommand _createQuestionCommand;
        private readonly ILogger<NewQuestionModel> _logger;

        public NewQuestionModel(CreateQuestionCommand createQuestionCommand, ILogger<NewQuestionModel> logger)
        {
            _createQuestionCommand = createQuestionCommand;
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(CreateQuestionRequest request)
        {
            _logger.LogInformation($"{DateTime.UtcNow.ToString("u")} - Received request to create a question");

            if (ModelState.IsValid)
            {
                var result = await _createQuestionCommand.Handle(request, Request.HttpContext.RequestAborted);
                if (result.Success)
                {
                    return Redirect($"/{request.Category}/question/${result.Id}");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("Server Error", error);
                    }
                }
            }
            return Page();
        }
    }
}
