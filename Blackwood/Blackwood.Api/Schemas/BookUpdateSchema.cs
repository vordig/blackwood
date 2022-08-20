namespace Blackwood.Api.Schemas;

public record BookUpdateSchema
{
    public string Title { get; init; } = string.Empty;
    public IEnumerable<string> Authors { get; init; } = Enumerable.Empty<string>();
    public IEnumerable<BookNoteSchema> Notes { get; init; } = Enumerable.Empty<BookNoteSchema>();
}