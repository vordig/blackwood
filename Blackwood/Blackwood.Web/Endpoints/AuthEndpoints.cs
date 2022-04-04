using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Blackwood.Core.Services;
using Blackwood.Web.Contracts.Requests;
using Blackwood.Web.Contracts.Responses;
using Blackwood.Web.Extensions;
using Blackwood.Web.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Blackwood.Web.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapGet("/auth/login", LoginAsync).AllowAnonymous();
        app.MapGet("/auth/account", AccountAsync).RequireAuthorization();
        app.MapGet("/auth/session/{id}", GetSessionId).RequireAuthorization();
    }

    private static async Task<IResult> LoginAsync(ISessionService sessionService, IMapper mapper, [FromBody] LoginWebRequest request)
    {
        var response = await sessionService.CreateSession(request.Email, request.Password);
        var result = mapper.Map<AuthWebResponse>(response);
        return Results.Json(result);
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
    
    private static async Task<IResult> GetSessionId(IMapper mapper, string id)
    {
        var sessionId = mapper.Map<long?>(id);
        return Results.Json(new { SessionId = sessionId });
    }
}