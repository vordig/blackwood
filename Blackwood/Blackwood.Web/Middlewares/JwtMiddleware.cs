using Blackwood.Core.Services;
using Blackwood.Web.Extensions;

namespace Blackwood.Web.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ISessionService _sessionService;

    public JwtMiddleware(RequestDelegate next, ISessionService sessionService)
    {
        _next = next;
        _sessionService = sessionService;
    }

    public async Task Invoke(HttpContext context)
    {
        var identity = context.User.Identity;
        if (identity is not null && identity.IsAuthenticated)
        {
            var sessionId = context.User.GetSessionId();
            var accessToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            await _sessionService.CheckSession(sessionId, accessToken);
        }

        await _next(context);
    }
}