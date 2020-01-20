using Codidact.Application.Common.Interfaces;
using Codidact.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Codidact.Application.Members.Commands.CreateMember
{
    public class CreateMemberCommand : IRequest<long>
    {

        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }

        public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, long>
        {
            private readonly IApplicationDbContext _context;

            public CreateMemberCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
            {
                var entity = new Member
                {
                    DisplayName = request.DisplayName,
                    Bio = request.Bio,
                    Email = request.Email,
                };

                _context.Members.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
