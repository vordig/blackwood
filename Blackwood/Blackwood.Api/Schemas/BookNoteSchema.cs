namespace Blackwood.Api.Schemas;

public record BookNoteSchema(string Title, IEnumerable<string> Authors);