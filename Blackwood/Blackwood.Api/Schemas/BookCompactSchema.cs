namespace Blackwood.Api.Schemas;

public record BookCompactSchema
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public IEnumerable<string> Authors { get; init; } = Enumerable.Empty<string>();
    public DateTime UpdatedAt { get; set; }
}