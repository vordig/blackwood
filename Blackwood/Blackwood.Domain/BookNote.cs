namespace Blackwood.Domain;

public record BookNote(string Title, IEnumerable<string> Authors);