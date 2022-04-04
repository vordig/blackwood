using System.Text;
using AutoMapper;
using Blackwood.Core.Services;
using Blackwood.Services;
using Blackwood.Services.Settings;
using Blackwood.Web.Endpoints;
using Blackwood.Web.Mapping;
using Blackwood.Web.Middlewares;
using Blackwood.Web.Settings;
using HashidsNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
Console.WriteLine($"Environment: {env}");

var builder = WebApplication.CreateBuilder(args);

var authSettings = builder.Configuration.GetSection("Auth").Get<AuthSettings>();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // enable for https
    options.SaveToken = false;

    var key = Encoding.ASCII.GetBytes(authSettings.JWTSecret);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = authSettings.JWTIssuer,
        ValidateAudience = true,
        ValidAudience = authSettings.JWTAudience,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("Admin", new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole("Admin").Build());
    config.AddPolicy("User", new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole("User").Build());
});

builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("Auth"));

var mapperConfig = new MapperConfiguration(mc =>
{
    var hashIdsSettings = builder.Configuration.GetSection("HashIds").Get<HashIdsSettings>();
    var hashIds = new Hashids(hashIdsSettings.Salt, hashIdsSettings.Length);
    mc.AddProfile(new MappingProfile(hashIds));
});

var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// change to transient
builder.Services.AddSingleton<ISessionService, SessionService>();

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<JwtMiddleware>();

app.MapGreetingsEndpoints();
app.MapAuthEndpoints();

app.Run();