using Blackwood.Domain;
using Blackwood.Infrastructure.Database;
using MongoDB.Driver;

namespace Blackwood.Infrastructure;

public class UserRepository
{
    private readonly DatabaseContext _databaseContext;

    public UserRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async ValueTask<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        const string collection = "users";
        var result = await _databaseContext.GetByIdAsync<User>(collection, id, cancellationToken)
            .ConfigureAwait(false);
        return result;
    }
    
    public async ValueTask<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        const string collection = "users";
        var filter = Builders<User>.Filter.Eq(user => user.Email, email);
        var result = await _databaseContext.GetOneAsync(collection, filter, cancellationToken)
            .ConfigureAwait(false);
        return result;
    }
    
    public async ValueTask<bool> IsUserExistAsync(string email, CancellationToken cancellationToken)
    {
        const string collection = "users";
        var filter = Builders<User>.Filter.Eq(user => user.Email, email);
        var result = await _databaseContext.CountAsync(collection, filter, cancellationToken)
            .ConfigureAwait(false);
        return result > 0;
    }
    
    public async ValueTask<User> CreateAsync(User user, CancellationToken cancellationToken)
    {
        const string collection = "users";
        var result = await _databaseContext.CreateAsync(collection, user, cancellationToken).ConfigureAwait(false);
        return result;
    }
}