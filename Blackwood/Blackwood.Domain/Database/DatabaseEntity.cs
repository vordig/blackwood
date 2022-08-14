namespace Blackwood.Domain.Database;

public record DatabaseEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Audit Audit { get; init; } = Audit.Created();
}