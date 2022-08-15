using Blackwood.Api.Endpoints;
using Blackwood.Infrastructure;
using Blackwood.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<DatabaseContext>();
builder.Services.AddScoped<BookRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI();

app.MapBookEndpoints();

app.Run();