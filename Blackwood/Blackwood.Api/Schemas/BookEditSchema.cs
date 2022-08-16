namespace Blackwood.Api.Schemas;

public record BookEditSchema
{
    public string Title { get; init; } = string.Empty;
    public IEnumerable<string> Authors { get; init; } = Enumerable.Empty<string>();
}