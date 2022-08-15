using Blackwood.Domain;
using Blackwood.Services;

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

    private static async Task<IResult> GetBooksAsync(BookService bookService, CancellationToken cancellationToken)
    {
        var result = await bookService.GetBooksAsync(cancellationToken);
        return Results.Json(result);
    }
    
    private static async Task<IResult> GetBookAsync(BookService bookService, Guid id, CancellationToken cancellationToken)
    {
        var result = await bookService.GetBookAsync(id, cancellationToken);
        return result is null ? Results.NotFound() : Results.Json(result);
    }
    
    private static async Task<IResult> CreateBookAsync(BookService bookService, Book book, CancellationToken cancellationToken)
    {
        var result = await bookService.CreateBookAsync(book, cancellationToken);
        var response = result;
        // var response = mapper.Map<CirqueDuSoleilOrderListItemResponse>(result);
        return Results.Created($"{BaseUrl}orders/{response.Id}", response);
    }
    
    private static async Task<IResult> UpdateBookAsync(BookService bookService, Guid id, Book book, CancellationToken cancellationToken)
    {
        var result = await bookService.UpdateBookAsync(book, cancellationToken);
        return result is null ? Results.NotFound() : Results.Json(result);
    }
    
    private static async Task<IResult> DeleteBookAsync(BookService bookService, Guid id, CancellationToken cancellationToken)
    {
        var result = await bookService.DeleteBookAsync(id, cancellationToken);
        return Results.Json(result);
    }
}