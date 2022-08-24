using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Blackwood.Domain;

public record Auth(string AccessToken, string? RefreshToken)
{
    public static Auth Generate(string issuer, string audience, string secret, DateTime expires,
        Dictionary<string, string> claims, string? refreshToken = null)
    {
        var accessToken = GenerateAccessToken(issuer, audience, secret, expires, claims);
        return new Auth(accessToken, refreshToken);
    }

    private static string GenerateAccessToken(string issuer, string audience, string secret, DateTime expires,
        Dictionary<string, string> claims)
    {
        var identity = new ClaimsIdentity(claims.Select(claim => new Claim(claim.Key, claim.Value)));
        var secretBytes = Encoding.UTF8.GetBytes(secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Issuer = issuer,
            Audience = audience,
            Expires = expires,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretBytes),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }
}