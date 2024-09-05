using Modsen.Server.CarsElections.Api;
using Modsen.Server.Shared.MiddlewareExtensions;
using Modsen.Server.CarsElections.Application;
using Modsen.Server.CarsElections.Infrastructure;
using Modsen.Server.Shared;
using Modsen.Server.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddInfrastructure(builder)
    .AddApplication()
    .AddShared(new JwtOptions
    {
        ValidAudience = builder.Configuration["Jwt:ValidAudience"]!,
        ValidIssuer = builder.Configuration["Jwt:ValidIssuer"]!,
        IssuerSigningKey = builder.Configuration["Jwt:IssuerSigningKey"]!
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandlerMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
