using Blackwood.Domain;
using Blackwood.Infrastructure;

namespace Blackwood.Services;

public class AuthService
{
    private readonly UserRepository _userRepository;

    public AuthService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async ValueTask<User> SignUpAsync(User user, CancellationToken cancellationToken)
    {
        if (await _userRepository.IsUserExistAsync(user.Email, cancellationToken))
            throw new ArgumentException($"User with email {user.Email} already exists");
        // user.HashPassword();
        var result = await _userRepository.CreateAsync(user, cancellationToken);
        return result;
    }

    public async ValueTask<Auth?> SignInAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(email, cancellationToken);
        if (user is null)
            return null;
        // if(!user.IsPasswordCorrect(password))
        //    return null;
        var claims = new Dictionary<string, string>
        {
            { "id", user.Id.ToString() },
            { "name", user.Name }
        };
        var result = Auth.Generate("Issuer", "Audience", "StrongSecretVeryVeryStrong123123123123123123", DateTime.UtcNow.AddMinutes(10), claims);
        return result;
    }
}