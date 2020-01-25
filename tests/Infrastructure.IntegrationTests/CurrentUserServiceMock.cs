using Codidact.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.IntegrationTests
{
    public class CurrentUserServiceMock : ICurrentUserService
    {
        public long GetMemberId()
        {
            return 1;
        }

        public string GetUserId()
        {
            return "1";
        }
    }
}
