using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Codidact.Application.Members
{
    public interface IMemberRepository
    {
        public Task<IEnumerable<MemberDto>> ListAsync(string filter = null);

        public Task<long> Create(MemberDto member);
    }
}
