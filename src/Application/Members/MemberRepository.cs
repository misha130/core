using AutoMapper;
using AutoMapper.QueryableExtensions;
using Codidact.Application.Common.Interfaces;
using Codidact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codidact.Application.Members
{
    public class MemberRepository : IMemberRepository
    {
        private IApplicationDbContext _context { get; }
        private IMapper _mapper { get; set; }

        public MemberRepository(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MemberDto>> ListAsync(string filter = null)
        {
            return await _context.Members
                     .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                     .OrderBy(m => m.DisplayName)
                     .ToListAsync().ConfigureAwait(false);
        }

        public async Task<long> Create(MemberDto member)
        {
            var entity = new Member
            {
                DisplayName = member.DisplayName,
                Bio = member.Bio,
                Email = member.Email,
            };

            _context.Members.Add(entity);

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return entity.Id;
        }
    }
}
