using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using Blackwood.Core.Domain;
using Blackwood.Core.Services;
using Blackwood.Core.Services.Contracts.Responses;
using Blackwood.Services.Helpers;
using Blackwood.Services.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Blackwood.Services;

public class SessionService : ISessionService
{
    private readonly AuthSettings _authSettings;

    private readonly Dictionary<long, Session> _sessions = new();

    public SessionService(IOptions<AuthSettings> authSettings)
    {
        _authSettings = authSettings.Value;
    }

    public async Task<CreateSessionServiceResponse> CreateSession(string email, string password)
    {
        // verify account by email and password and get it
        var accountId = 11;
        var accountRole = "User";
        var sessionId = new Random().NextInt64(1, 10000);
        var session = new Session
        {
            Id = sessionId,
            AccountId = accountId,
            IssuedAt = DateTime.UtcNow,
            ExpiredAt = DateTime.UtcNow.AddDays(_authSettings.RefreshTokenTimeToLive),
            AccessTokenHash = string.Empty,
            RefreshToken = string.Empty
        };
        // create document in db to get session id
        var accessToken = GenerateAccessToken(session.Id, accountRole);
        session.AccessTokenHash = HashHelper.ComputeHash(accessToken);
        // update session document in db
        _sessions[sessionId] = session;
        var result = new CreateSessionServiceResponse
        {
            AccessToken = accessToken,
            RefreshToken = session.RefreshToken
        };
        return result;
    }

    public Task<CreateSessionServiceResponse> CreateSession(long accountId)
    {
        throw new NotImplementedException();
    }

    public async Task<long> GetAccountId(long sessionId)
    {
        var session = _sessions.GetValueOrDefault(sessionId);
        return session.AccountId;
    }

    public async Task CheckSession(long sessionId, string accessToken)
    {
        var session = _sessions.GetValueOrDefault(sessionId);
        if (session is null)
            throw new AuthenticationException("Session closed");
        if(DateTime.UtcNow > session.ExpiredAt)
            throw new AuthenticationException("Session expired");
        if(!HashHelper.VerifyHash(accessToken, session.AccessTokenHash))
            throw new AuthenticationException("Access token is not valid");
    }

    private string GenerateAccessToken(long sessionId, string role)
    {
        var issuer = _authSettings.JWTIssuer;
        var audience = _authSettings.JWTAudience;
        var expires = DateTime.UtcNow.AddMinutes(_authSettings.JWTTimeToLive);
        var secret = Encoding.UTF8.GetBytes(_authSettings.JWTSecret);
        var identity = new ClaimsIdentity(new List<Claim>()
        {
            new("sessionId", sessionId.ToString()),
            new("role", role)
        });
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Issuer = issuer,
            Audience = audience,
            Expires = expires,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }
}