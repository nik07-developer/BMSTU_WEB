using Handlers.User;
using Handlers.Character;
using Handlers.View;

using MongoRepository;

using MongoDB.Driver;
using MongoDB.Bson;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataAccess.Repositories;
using DataAccess.Interfaces;

var cli = new MongoClient(MongoConfig.DB_ADDRESS);
foreach(var a in cli.ListDatabaseNames().ToList())
{
    Console.WriteLine(a);
}

const string allowAllPolicy = "_allow_all";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<CreateUserHandler>();
builder.Services.AddSingleton<UpdateUserHandler>();
builder.Services.AddSingleton<DeleteUserHandler>();
builder.Services.AddSingleton<GetUserHandler>();

builder.Services.AddSingleton<ICharacterRepository, CharacterRepository>();
builder.Services.AddSingleton<CreateCharacterHandler>();
builder.Services.AddSingleton<UpdateCharacterHandler>();
builder.Services.AddSingleton<DeleteCharacterHandler>();
builder.Services.AddSingleton<GetCharactersHandler>();
builder.Services.AddSingleton<GetCharacterHandler>();

builder.Services.AddSingleton<ICharacterViewRepository, CharacterViewRepository>();
builder.Services.AddSingleton<CreateViewHandler>();
builder.Services.AddSingleton<UpdateViewHandler>();
builder.Services.AddSingleton<DeleteViewHandler>();
builder.Services.AddSingleton<GetViewHandler>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(allowAllPolicy, policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });

var app = builder.Build();

app.UseCors(allowAllPolicy);
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

public class AuthOptions
{
    public const string ID_CLAIM_TYPE = "id";
    public const string ISSUER = "MyAuthServer";
    public const string AUDIENCE = "MyAuthClient";
    const string KEY = "mysupersecret_secretkey!123";
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}
