namespace Codidact.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        public string GetUserId();
        public long GetMemberId();
    }
}
