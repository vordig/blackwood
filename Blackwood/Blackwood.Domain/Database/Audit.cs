namespace Blackwood.Domain.Database;

public record Audit(DateTime CreatedAt, DateTime UpdatedAt)
{
    public static Audit Created() => new(DateTime.UtcNow, DateTime.UtcNow);
    public static Audit Updated(Audit audit) => audit with { UpdatedAt = DateTime.UtcNow };
}