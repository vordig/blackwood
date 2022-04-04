using System.Security.Authentication;
using System.Security.Claims;

namespace Blackwood.Web.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static long GetSessionId(this ClaimsPrincipal user)
    {
        try
        {
            var sessionId = long.Parse(user.Identities.First().Claims.First(x => x.Type == "sessionId").Value);
            return sessionId;
        }
        catch (Exception e)
        {
            throw new AuthenticationException("Can not verify a session");
        }
    }
}