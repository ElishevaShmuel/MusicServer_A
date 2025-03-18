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


// Add services to the container.

builder.Services.AddControllers();
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


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();




builder.Services.AddAuthorization();


app.UseAuthorization();

app.MapControllers();

app.Run();
