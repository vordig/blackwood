using Blackwood.Domain.Database;

namespace Blackwood.Domain;

public record Book : DatabaseEntity
{
    public string Title { get; init; }
    public IEnumerable<string> Authors { get; init; }
    public IEnumerable<BookNote> Notes { get; set; }
}