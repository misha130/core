using System.Threading.Tasks;
using Codidact.Core.Application.Members;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Codidact.Core.Api
{
    [Authorize]
    public class MemberService : Member.MemberBase
    {
        private readonly ILogger<MemberService> _logger;
        private readonly IMembersRepository _memberRepository;
        public MemberService(ILogger<MemberService> logger, IMembersRepository memberRepository)
        {
            _logger = logger;
            _memberRepository = memberRepository;
        }

        public override async Task<MemberCreationReply> CreateMember(MemberCreationRequest request, ServerCallContext context)
        {
            _ = await _memberRepository.Create(new Domain.Entities.Member
            {
                DisplayName = request.DisplayName,
                UserId = request.UserId,
            });

            return new MemberCreationReply
            {
                Success = true,
            };
        }
    }
}
