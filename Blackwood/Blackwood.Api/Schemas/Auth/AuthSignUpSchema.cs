namespace Blackwood.Api.Schemas.Auth;

public record AuthSignUpSchema
{
    public string Name { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}