namespace Blackwood.Api.Schemas;

public record BookEditNoteSchema()
{
    public string Title { get; init; } = string.Empty;
    public IEnumerable<string> Authors { get; init; } = Enumerable.Empty<string>();
}