namespace Blackwood.Web.Contracts.Responses;

public class ErrorWebResponse
{
    public List<string> Errors { get; set; } = new();

    public ErrorWebResponse()
    {
    }

    public ErrorWebResponse(string error) => Errors.Add(error);
}