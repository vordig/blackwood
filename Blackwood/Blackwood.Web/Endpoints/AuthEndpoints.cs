using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blackwood.Core.Services;
using Blackwood.Web.Contracts.Responses;
using Blackwood.Web.Extensions;
using Blackwood.Web.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Blackwood.Web.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapGet("/auth/login", LoginAsync).AllowAnonymous();
        app.MapGet("/auth/account", AccountAsync).RequireAuthorization();
    }

    private static async Task<IResult> LoginAsync(ISessionService sessionService, string email, string password)
    {
        var response = await sessionService.CreateSession(email, password);
        // var response = new AuthWebResponse
        // {
        //     AccessToken = GenerateAccessToken(authSettings.Value, 11),
        //     RefreshToken = "test refresh token"
        // };
        return Results.Json(response);
    }

    private static async Task<IResult> AccountAsync(ClaimsPrincipal user, ISessionService sessionService)
    {
        var accountId = await sessionService.GetAccountId(user.GetSessionId());
        await Task.Run(() => Console.WriteLine($"AccountId: {accountId}"));
        return Results.Created("auth/account/2", 10);
        //throw new BadHttpRequestException("asdas");
        return Results.BadRequest(new ErrorWebResponse("Oh no"));
        //return Results.Ok();
    }
}