using Blackwood.Domain;
using Blackwood.Infrastructure.Database;

namespace Blackwood.Infrastructure;

public class BookRepository
{
    private readonly DatabaseContext _databaseContext;

    public BookRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async ValueTask<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken)
    {
        const string collection = "books";
        var result = await _databaseContext.GetAllAsync<Book>(collection, cancellationToken)
            .ConfigureAwait(false);
        return result;
    }

    public async ValueTask<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        const string collection = "books";
        var result = await _databaseContext.GetByIdAsync<Book>(collection, id, cancellationToken)
            .ConfigureAwait(false);
        return result;
    }

    public async ValueTask<Book> CreateAsync(Book book, CancellationToken cancellationToken)
    {
        const string collection = "books";
        var result = await _databaseContext.CreateAsync(collection, book, cancellationToken).ConfigureAwait(false);
        return result;
    }

    public async ValueTask<Book?> UpdateAsync(Book book, CancellationToken cancellationToken)
    {
        const string collection = "books";
        var result = await _databaseContext.UpdateAsync(collection, book, cancellationToken).ConfigureAwait(false);
        return result;
    }

    public async ValueTask<long> DeleteAsync(Guid bookId, CancellationToken cancellationToken)
    {
        const string collection = "books";
        var result = await _databaseContext.DeleteAsync<Book>(collection, bookId, cancellationToken)
            .ConfigureAwait(false);
        return result;
    }
}