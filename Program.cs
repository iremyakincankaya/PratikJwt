using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PratikJwt.Context;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<JwtDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext")));



// Jwt ayarlar� i�in
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // appsettings de verdi�imiz jwt parametreleriyle e�le�iyormu onlar� kontrol ediyoruz
        ValidIssuer = builder.Configuration["Jwt:Issuer"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],

        ValidateLifetime = true, // S�resi ge�en token� kabul etme
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
    };

});





builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Swager ekran�nda token girebilmek i�in 
builder.Services.AddSwaggerGen(option =>
{
    var jwtSecurtiyScheme = new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Name = "Jwt Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "A�a��daki textboxa jwt token yap��t�r.",
        Reference = new OpenApiReference { Id = JwtBearerDefaults.AuthenticationScheme, Type = ReferenceType.SecurityScheme },

    };

    option.AddSecurityDefinition(jwtSecurtiyScheme.Reference.Id, jwtSecurtiyScheme);
    option.AddSecurityRequirement(new OpenApiSecurityRequirement

    {
        {jwtSecurtiyScheme,Array.Empty<string>() }
    });

});




builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//token i�in bu middlewarei ekliyoruz
app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();

app.Run();
