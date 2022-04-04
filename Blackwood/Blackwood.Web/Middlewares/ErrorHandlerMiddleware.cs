using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using Blackwood.Web.Contracts.Responses;

namespace Blackwood.Web.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = ex switch
            {
                // additional handling
                AuthenticationException => (int)HttpStatusCode.Unauthorized,
                BadHttpRequestException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError,
            };
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var result = JsonSerializer.Serialize(new ErrorWebResponse(ex.Message), options);
            await response.WriteAsync(result);
        }
    }
}