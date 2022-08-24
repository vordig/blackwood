using AutoMapper;
using Blackwood.Api.Schemas.Book;
using Blackwood.Domain;
using Blackwood.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blackwood.Api.Endpoints;

public static class BookEndpoints
{
    private const string BaseUrl = "api/books/";

    public static void MapBookEndpoints(this WebApplication app)
    {
        app.MapGet("api/books", GetBooksAsync);
        app.MapGet("api/books/{id:guid}", GetBookAsync);
        app.MapPost("api/books", CreateBookAsync);
        app.MapPut("api/books/{id:guid}", UpdateBookAsync);
        app.MapDelete("api/books/{id:guid}", DeleteBookAsync);
    }

    [ProducesResponseType(typeof(IEnumerable<BookCompactSchema>), 200)]
    private static async Task<IResult> GetBooksAsync(BookService bookService, IMapper mapper,
        CancellationToken cancellationToken)
    {
        var result = await bookService.GetBooksAsync(cancellationToken);
        var response = mapper.Map<IEnumerable<BookCompactSchema>>(result);
        return Results.Json(response);
    }

    private static async Task<IResult> GetBookAsync(BookService bookService, IMapper mapper, Guid id,
        CancellationToken cancellationToken)
    {
        var result = await bookService.GetBookAsync(id, cancellationToken);
        if (result is null)
            return Results.NotFound();
        var response = mapper.Map<BookDetailedSchema>(result);
        return Results.Json(response);
    }

    private static async Task<IResult> CreateBookAsync(BookService bookService, IMapper mapper,
        BookUpdateSchema bookUpdate, CancellationToken cancellationToken)
    {
        var book = mapper.Map<Book>(bookUpdate);
        var result = await bookService.CreateBookAsync(book, cancellationToken);
        var response = mapper.Map<BookNotificationSchema>(result);
        return Results.Created($"{BaseUrl}{response.Id}", response);
    }

    private static async Task<IResult> UpdateBookAsync(BookService bookService, IMapper mapper, Guid id,
        BookUpdateSchema bookUpdate, CancellationToken cancellationToken)
    {
        var book = mapper.Map<Book>(bookUpdate);
        var result = await bookService.UpdateBookAsync(book, cancellationToken);
        var response = mapper.Map<BookNotificationSchema>(result);
        return result is null ? Results.NotFound() : Results.Json(response);
    }

    private static async Task<IResult> DeleteBookAsync(BookService bookService, Guid id,
        CancellationToken cancellationToken)
    {
        var result = await bookService.DeleteBookAsync(id, cancellationToken);
        return Results.Json(result);
    }
}