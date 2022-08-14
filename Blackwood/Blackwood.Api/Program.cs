using Blackwood.Api.Endpoints;
using Blackwood.Infrastructure;
using Blackwood.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<DatabaseContext>();
builder.Services.AddScoped<BookRepository>();

var app = builder.Build();

app.UseRouting();

app.MapBookEndpoints();

app.Run();