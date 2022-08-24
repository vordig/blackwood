﻿namespace Blackwood.Api.Schemas.Book;

public record BookNotificationSchema
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Authors { get; init; } = string.Empty;
}