namespace Blackwood.Core.Domain;

public class Session
{
    public long Id { get; set; }
    public long AccountId { get; set; }
    public DateTime IssuedAt { get; set; }
    public DateTime ExpiredAt { get; set; }
    public string AccessTokenHash { get; set; }
    public string RefreshToken { get; set; }
}