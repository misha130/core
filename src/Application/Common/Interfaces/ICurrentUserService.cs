namespace Codidact.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        public string UserId { get; }
        public long MemberId { get; }
    }
}
