using Handlers.User;
using Handlers.Character;

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


const string allowAllPolicy = "_allow_all";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUserRepository, UserRepositoryStub>();
builder.Services.AddSingleton<CreateUserHandler>();
builder.Services.AddSingleton<UpdateUserHandler>();
builder.Services.AddSingleton<DeleteUserHandler>();
builder.Services.AddSingleton<GetUserHandler>();

builder.Services.AddSingleton<ICharacterRepository, CharacterRepositoryStub>();
builder.Services.AddSingleton<CreateCharacterHandler>();
builder.Services.AddSingleton<UpdateCharacterHandler>();
builder.Services.AddSingleton<DeleteCharacterHandler>();
builder.Services.AddSingleton<GetCharactersHandler>();
builder.Services.AddSingleton<GetCharacterHandler>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Нужно для взаимодействия на разных доменах.
// В нашем случае localhost:3000 и localhost:5000
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
app.UseAuthorization();  // ... уже была в строке 46?

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseAuthorization();  // ... уже была в строке 66?
app.MapControllers();
app.Run();

public class AuthOptions
{
    public const string ID_CLAIM_TYPE = "id";
    public const string ISSUER = "MyAuthServer";
    public const string AUDIENCE = "MyAuthClient";
    const string KEY = "mysupersecret_secretkey!123";  // я стырил
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}
