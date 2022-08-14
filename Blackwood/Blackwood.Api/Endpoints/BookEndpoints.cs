using Blackwood.Domain;

namespace Blackwood.Api.Endpoints;

public static class BookEndpoints
{
    public static void MapBookEndpoints(this WebApplication app)
    {
        app.MapGet("api/books", GetBooksAsync);
        app.MapGet("api/books/{id:guid}", GetBookAsync);
        app.MapPost("api/books", CreateBookAsync);
        app.MapPut("api/books/{id:guid}", UpdateBookAsync);
        app.MapDelete("api/books/{id:guid}", DeleteBookAsync);
    }

    private static async Task<IResult> GetBooksAsync(CancellationToken cancellationToken)
    {
        return Results.Ok();
        // throw new NotImplementedException();
        // var result = await cirqueDuSoleilService.GetSeriesAsync(cancellationToken);
        // return Results.Json(result);
    }
    
    private static async Task<IResult> GetBookAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    private static async Task<IResult> CreateBookAsync(Book book, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    private static async Task<IResult> UpdateBookAsync(Guid id, Book book, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    private static async Task<IResult> DeleteBookAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}