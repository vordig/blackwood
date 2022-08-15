using Blackwood.Domain;
using Blackwood.Infrastructure;

namespace Blackwood.Services;

public class BookService
{
    private readonly BookRepository _bookRepository;

    public BookService(BookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async ValueTask<IEnumerable<Book>> GetBooksAsync(CancellationToken cancellationToken)
    {
        var result = await _bookRepository.GetAllAsync(cancellationToken);
        return result;
    }
    
    public async ValueTask<Book?> GetBookAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _bookRepository.GetByIdAsync(id, cancellationToken);
        return result;
    }
    
    public async ValueTask<Book> CreateBookAsync(Book book, CancellationToken cancellationToken)
    {
        var result = await _bookRepository.CreateAsync(book, cancellationToken);
        return result;
    }
    
    public async ValueTask<Book?> UpdateBookAsync(Book book, CancellationToken cancellationToken)
    {
        var result = await _bookRepository.UpdateAsync(book, cancellationToken);
        return result;
    }
    
    public async ValueTask<long> DeleteBookAsync(Guid bookId, CancellationToken cancellationToken)
    {
        var result = await _bookRepository.DeleteAsync(bookId, cancellationToken);
        return result;
    }
}