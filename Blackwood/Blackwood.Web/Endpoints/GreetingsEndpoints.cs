using Blackwood.Web.Contracts.Requests;
using Blackwood.Web.Contracts.Responses;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;

namespace Blackwood.Web.Endpoints;

public static class GreetingsEndpoints
{
    public static void MapGreetingsEndpoints(this WebApplication app)
    {
        app.MapGet("/greetings", Greeting);
        // app.MapGet("/greetings/{id}", GreetingByIntId);
        // app.MapGet("/greetings/decode/{id}", GreetingDecodeId);
    }
    
    private static async Task<IResult> GreetingDecodeId(IHashids hashids, string id)
    {
        var rawId = hashids.Decode(id);
        return rawId.Length == 0 ? Results.NotFound() : Results.Json(new { Id = rawId[0] });
    }
    
    private static async Task<IResult> GreetingByIntId(IHashids hashids, int id)
    {
        return Results.Json(hashids.Encode(id));
    }

    private static async Task<IResult> GreetingById(string id)
    {
        return Results.Json(id);
    }

    internal static async Task<IResult> Greeting(string firstName, string lastName)
    {
        var response = new GreetingsWebResponse
        {
            GreetingsMessage = $"Hello {firstName} {lastName}"
        };
        return Results.Json(response);
    }
}