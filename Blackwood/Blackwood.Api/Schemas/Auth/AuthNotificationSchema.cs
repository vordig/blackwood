namespace Blackwood.Api.Schemas.Auth;

public record AuthNotificationSchema
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
}