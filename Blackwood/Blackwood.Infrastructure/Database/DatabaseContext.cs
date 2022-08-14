using Blackwood.Domain.Database;
using MongoDB.Driver;

namespace Blackwood.Infrastructure.Database;

public class DatabaseContext
{
    private readonly IMongoDatabase _database;

    public DatabaseContext(IMongoDatabase database)
    {
        _database = database;
    }

    public IMongoCollection<TEntity> Collection<TEntity>(string collection) where TEntity : DatabaseEntity =>
        _database.GetCollection<TEntity>(collection);
}