using GlobalRenov.Datas;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using GlobalRenov.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity;
using GlobalRenov.Models;

var builder = WebApplication.CreateBuilder(args);

// Ajout des services au conteneur
builder.Services.AddControllers();
// En savoir plus sur la configuration de Swagger/OpenAPI à https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

// Gestion de la connexion à la base de données
var connectionString = configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DefaultContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Gestion du token JWT
var jwtKey = configuration["AppSettings:JwtKey"];
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// Ajout des services CORS et définition de la politique CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactAppOrigin",
        builder => builder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());
});


// Configuration de la base de données
/*
builder.Services.AddDbContext<DefaultContext>(options => 
{
    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GlobalRenov;Trusted_Connection=True;MultipleActiveResultSets=true");
});
*/

var app = builder.Build();

// Configuration du pipeline de requêtes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

// Application de la politique CORS
app.UseCors("AllowReactAppOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
