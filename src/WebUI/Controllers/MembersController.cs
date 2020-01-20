using Codidact.Application.Members;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Codidact.WebUI.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMemberRepository _memberRepository;
        public MembersController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _memberRepository.ListAsync());
        }
    }
}