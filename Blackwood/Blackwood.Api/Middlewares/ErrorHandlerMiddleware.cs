using System.Net;
using System.Text.Json;
using Blackwood.Api.Schemas;

namespace Blackwood.Api.Middlewares;

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
                ArgumentException => (int) HttpStatusCode.BadRequest,
                _ => (int) HttpStatusCode.InternalServerError,
            };
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var result = JsonSerializer.Serialize(new NotificationSchema(ex.Message), options);
            await response.WriteAsync(result);
        }
    }
}