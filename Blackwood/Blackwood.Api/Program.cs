using AutoMapper;
using Blackwood.Api.Endpoints;
using Blackwood.Api.Mapping;
using Blackwood.Infrastructure;
using Blackwood.Infrastructure.Database;
using Blackwood.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new BookProfile());
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddSingleton(typeof(IMongoDatabase), _ =>
{
    var connectionString = builder.Configuration["Database:ConnectionString"];
    var url = MongoUrl.Create(connectionString);
    var client = new MongoClient(connectionString);
    return client.GetDatabase(url.DatabaseName);
});

builder.Services.AddScoped<DatabaseContext>();
builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped<BookService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI();

app.MapBookEndpoints();

app.Run();