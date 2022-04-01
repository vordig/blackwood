namespace Blackwood.Web.Settings;

public class AuthSettings
{
    public string JWTSecret { get; set; }
    public string JWTIssuer { get; set; }
    public string JWTAudience { get; set; }
    public int JWTTimeToLive { get; set; }
    public int RefreshTokenTimeToLive { get; set; }
}