using Blackwood.Infrastructure.Database;

namespace Blackwood.Infrastructure;

public class BookRepository
{
    private readonly DatabaseContext _databaseContext;

    public BookRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
}