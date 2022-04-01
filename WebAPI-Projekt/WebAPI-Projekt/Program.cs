using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Projekt.APIAuth;
using WebAPI_Projekt.Data;
using WebAPI_Projekt.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GeoMessageDbContext>(
   options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddInMemoryApiResources(Configuration.ApiResources)
    .AddInMemoryClients(Configuration.Clients);

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<GeoMessageDbContext>();


builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.Audience = "auth.web.api";
        options.Authority = "https://localhost:44364";
    });

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy =
         new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
