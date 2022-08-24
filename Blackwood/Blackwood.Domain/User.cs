using Blackwood.Domain.Database;

namespace Blackwood.Domain;

public record User : DatabaseEntity
{
    public string Name { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}