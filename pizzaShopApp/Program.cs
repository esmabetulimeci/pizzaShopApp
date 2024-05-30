using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.Repositories.MailProviders;
using Application.Common.Interfaces.Jwt;
using Application.Common.Interfaces.Mail;
using Application.Services.Jwt;
using Application.Services.ExchangeRate;
using Application.Common.Interfaces.Redis;
using Infrastructure.Repositories.Redis;
using Application.Common.Interfaces;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<IPizzaShopAppDbContext, PizzaShopAppDbContext>();
builder.Services.AddScoped<IRedisDbContext, RedisDbContext>();
builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(IPizzaShopAppDbContext).Assembly));
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddSingleton<IMailProviderFactory, MailProviderFactory>();
builder.Services.AddSingleton<TcmbService>();
builder.Services.AddHttpClient<TcmbService>();
builder.Services.AddSingleton<IJwtService, JwtService>(provider => new JwtService(builder.Configuration["Jwt:SecretKey"]));


// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
    };
});


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

