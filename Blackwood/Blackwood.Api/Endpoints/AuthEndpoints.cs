using AutoMapper;
using Blackwood.Api.Schemas.Auth;
using Blackwood.Domain;
using Blackwood.Services;

namespace Blackwood.Api.Endpoints;

public static class AuthEndpoints
{
    private const string BaseUrl = "api/auth/";

    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("api/auth/sign-in", SignInAsync);
        app.MapPost("api/auth/sign-up", SignUpAsync);
    }

    private static async Task<IResult> SignInAsync(AuthService authService, AuthSignInSchema schema, IMapper mapper,
        CancellationToken cancellationToken)
    {
        var result = await authService.SignInAsync(schema.Email, schema.Password, cancellationToken);
        var response = mapper.Map<AuthSchema>(result);
        return result is null ? Results.BadRequest() : Results.Json(response);
    }

    private static async Task<IResult> SignUpAsync(AuthService authService, AuthSignUpSchema schema, IMapper mapper,
        CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(schema);
        var result = await authService.SignUpAsync(user, cancellationToken);
        var response = mapper.Map<AuthNotificationSchema>(result);
        return Results.Created($"{BaseUrl}sign-in", response);
    }
}