using Blackwood.Web.Endpoints;
using Blackwood.Web.Settings;
using HashidsNet;

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
Console.WriteLine($"Environment: {env}");

var builder = WebApplication.CreateBuilder(args);

var hashIdsSettings = builder.Configuration.GetSection("HashIdsSettings").Get<HashIdsSettings>();
builder.Services.AddSingleton<IHashids>(_ => new Hashids(hashIdsSettings.Salt, hashIdsSettings.Length));

var app = builder.Build();

app.MapGet("helloworld", () => "Hello, World!");

app.UseRouting();
app.MapGreetingsEndpoints();

app.Run();