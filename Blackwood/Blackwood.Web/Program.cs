using System.Text;
using Blackwood.Core.Services;
using Blackwood.Services;
using Blackwood.Services.Settings;
using Blackwood.Web.Endpoints;
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

var hashIdsSettings = builder.Configuration.GetSection("HashIds").Get<HashIdsSettings>();
builder.Services.AddSingleton<IHashids>(_ => new Hashids(hashIdsSettings.Salt, hashIdsSettings.Length));

// instead of adding IHashIds to DI I will add it to mapping profile via constructor
// IHashIds will be used only via mapping

// var mapperConfig = new MapperConfiguration(mc =>
// {
//     mc.AddProfile(new MappingProfile());
//     mc.AddProfile(new SkyboxMappingProfile());
// });
//
// IMapper mapper = mapperConfig.CreateMapper();
// services.AddSingleton(mapper);

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