using AutoMapper;
using AutoMapper.QueryableExtensions;
using Codidact.Application.Common.Interfaces;
using Codidact.Application.Members.Queries.ListMembers;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Codidact.Application.Members.Queries
{
    public class ListMembersQuery : IRequest<IEnumerable<MemberDto>>
    {
        public string Search { get; set; }

        public class ListMembersQueryHandler : IRequestHandler<ListMembersQuery, IEnumerable<MemberDto>>
        {

            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public ListMembersQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<MemberDto>> Handle(ListMembersQuery request, CancellationToken cancellationToken)
            {
                return await _context.Members
                     .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                     .OrderBy(m => m.DisplayName)
                     .ToListAsync(cancellationToken);
            }
        }
    }
}
