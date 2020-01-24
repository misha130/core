using Codidact.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Codidact.WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            string memberId = httpContextAccessor.HttpContext.User.FindFirstValue("memberId");
            if (!string.IsNullOrEmpty(memberId))
            {
                MemberId = long.Parse(memberId);
            }
        }

        public string UserId { get; }
        public long MemberId { get; }
    }
}
