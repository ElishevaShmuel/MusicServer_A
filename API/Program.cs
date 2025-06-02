
using Amazon.S3;
using Amazon.Extensions.NETCore.Setup;
using Core.Irepository;
using Core.Iservice;
using Data.data;
using Data.repositories;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Service.services;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();


// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DataContaxt>();

builder.Services.AddScoped<IserUser, serUser>();
builder.Services.AddScoped<IserCurrency, serCurrency>();
builder.Services.AddScoped<IserAudioFile, serAudioFile>();

builder.Services.AddScoped<IrepUser, RepUser>();
builder.Services.AddScoped<IrepCurrency, repCurrency>();
builder.Services.AddScoped<IrepAudioFile, repAudioFile>();


//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = false,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer =Environment.GetEnvironmentVariable("ISSUER"),
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("KEY")))
//        };
//    });

var key = Environment.GetEnvironmentVariable("JWT__KEY");
if (string.IsNullOrEmpty(key))
{
    throw new InvalidOperationException("Environment variable 'KEY' must be set and not null.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = false,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = Environment.GetEnvironmentVariable("JWT__ISSUER"),
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
       };
   });

builder.Services.AddCors(); 



builder.Services.AddAuthorization();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.MapGet("/", () => "Welcome to the Audio File Management API!");

app.Run();
