using Blackwood.Core.Services.Contracts.Responses;

namespace Blackwood.Core.Services;

public interface ISessionService
{
    public Task<CreateSessionServiceResponse> CreateSession(string email, string password);
    public Task<CreateSessionServiceResponse> CreateSession(long accountId);
    public Task<long> GetAccountId(long sessionId);
    public Task CheckSession(long sessionId, string accessToken);
}