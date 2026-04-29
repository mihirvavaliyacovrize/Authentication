//using JwtAuthAPI.Data;

//using JwtAuthAPI.Repositories;
//using JwtAuthAPI.Repositories.Interfaces;

//using JwtAuthAPI.Services;
//using JwtAuthAPI.Services.Interfaces;

//using Microsoft.EntityFrameworkCore;

//var builder =
//WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();

//builder.Services.AddDbContext<
//AppDbContext>(options =>
//options.UseSqlServer(
//builder.Configuration
//.GetConnectionString(
//"DefaultConnection")));

//builder.Services.AddScoped<
//IUserRepository,
//UserRepository>();

//builder.Services.AddScoped<
//IUserService,
//UserService>();
//builder.Services.AddScoped<
//IJobRepository,
//JobRepository>();

//builder.Services.AddScoped<
//IJobService,
//JobService>();

//var app = builder.Build();

//app.MapControllers();

//app.Run();

using JwtAuthAPI.Data;
using JwtAuthAPI.Repositories;
using JwtAuthAPI.Repositories.Interfaces;
using JwtAuthAPI.Services;
using JwtAuthAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();



builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(
builder.Configuration.GetConnectionString(
"DefaultConnection"
)));

builder.Services.AddScoped<
IUserRepository,
UserRepository>();

builder.Services.AddScoped<
IUserService,
UserService>();

builder.Services.AddScoped<
IJobRepository,
JobRepository>();

builder.Services.AddScoped<
IJobService,
JobService>();


// JWT
builder.Services
.AddAuthentication(
JwtBearerDefaults.AuthenticationScheme
)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
    new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer =
      builder.Configuration["Jwt:Issuer"],

        ValidAudience =
      builder.Configuration["Jwt:Audience"],

        IssuerSigningKey =
      new SymmetricSecurityKey(
      Encoding.UTF8.GetBytes(
      builder.Configuration["Jwt:Key"]!
      ))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();