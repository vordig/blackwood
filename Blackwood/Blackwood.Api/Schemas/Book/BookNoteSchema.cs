namespace Blackwood.Api.Schemas.Book;

public record BookNoteSchema(string Title, IEnumerable<string> Authors);