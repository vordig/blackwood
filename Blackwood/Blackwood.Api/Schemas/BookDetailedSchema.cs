namespace Blackwood.Api.Schemas;

public record BookDetailedSchema
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public IEnumerable<string> Authors { get; init; } = Enumerable.Empty<string>();
    public IEnumerable<BookNoteSchema> Notes { get; init; } = Enumerable.Empty<BookNoteSchema>();
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}