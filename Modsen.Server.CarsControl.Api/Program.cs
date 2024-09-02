using Modsen.Server.CarsControl.DataAccess;
using Modsen.Server.CarsControl.Business;
using Modsen.Server.CarsControl.Api;
using Modsen.Server.Shared.MiddlewareExtensions;
using Modsen.Server.Shared;
using Modsen.Server.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresintation()
    .AddDataAccess(builder)
    .AddBusiness()
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
