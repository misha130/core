using Codidact.Application.Members.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Codidact.WebUI.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMediator _mediator;
        public MemberController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _mediator.Send(new ListMembersQuery()));
        }
    }
}