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

    private IMongoCollection<TEntity> Collection<TEntity>(string collection) where TEntity : DatabaseEntity =>
        _database.GetCollection<TEntity>(collection);

    public async ValueTask<IEnumerable<TEntity>> GetAllAsync<TEntity>(string collection,
        CancellationToken cancellationToken) where TEntity : DatabaseEntity
    {
        var result = await Collection<TEntity>(collection).Find(Builders<TEntity>.Filter.Empty)
            .ToListAsync(cancellationToken);
        return result;
    }

    public async ValueTask<TEntity?> GetByIdAsync<TEntity>(string collection, Guid id,
        CancellationToken cancellationToken) where TEntity : DatabaseEntity
    {
        var result = await Collection<TEntity>(collection).Find(entity => entity.Id == id)
            .SingleOrDefaultAsync(cancellationToken);
        return result;
    }

    public async ValueTask<TEntity> CreateAsync<TEntity>(string collection, TEntity entity,
        CancellationToken cancellationToken) where TEntity : DatabaseEntity
    {
        await Collection<TEntity>(collection).InsertOneAsync(entity, null, cancellationToken);
        return entity;
    }

    public async ValueTask<TEntity?> UpdateAsync<TEntity>(string collection, TEntity entity,
        CancellationToken cancellationToken) where TEntity : DatabaseEntity
    {
        var result = await Collection<TEntity>(collection)
            .FindOneAndReplaceAsync(x => x.Id == entity.Id, entity, null, cancellationToken);
        return result;
    }

    public async ValueTask<long> DeleteAsync<TEntity>(string collection, Guid id, CancellationToken cancellationToken)
        where TEntity : DatabaseEntity
    {
        var result = await Collection<TEntity>(collection).DeleteOneAsync(x => x.Id == id, cancellationToken)
            .ConfigureAwait(false);
        return result.DeletedCount;
    }

    public async ValueTask<TEntity?> GetOneAsync<TEntity>(string collection, FilterDefinition<TEntity> filter,
        CancellationToken cancellationToken) where TEntity : DatabaseEntity
    {
        var result = await Collection<TEntity>(collection).Find(filter).SingleOrDefaultAsync(cancellationToken);
        return result;
    }
    
    public async ValueTask<long> CountAsync<TEntity>(string collection, FilterDefinition<TEntity> filter,
        CancellationToken cancellationToken) where TEntity : DatabaseEntity
    {
        var result = await Collection<TEntity>(collection).Find(filter).CountDocumentsAsync(cancellationToken);
        return result;
    }
}