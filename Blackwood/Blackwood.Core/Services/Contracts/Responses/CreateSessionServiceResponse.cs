namespace Blackwood.Core.Services.Contracts.Responses;

public class CreateSessionServiceResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}