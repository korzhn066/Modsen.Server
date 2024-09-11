using Modsen.Server.CarsElections.Api;
using Modsen.Server.CarsElections.Api.Services;
using Modsen.Server.CarsElections.Application;
using Modsen.Server.CarsElections.Infrastructure;
using Modsen.Server.Shared.MiddlewareExtensions;
using Modsen.Server.Shared;
using Modsen.Server.Shared.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .ConfigureLogger(builder.Configuration["ELK:LogstashUri"]!);

builder.Services
    .AddPresentation()
    .AddInfrastructure(builder)
    .AddApplication(builder.Configuration)
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

app.MapGrpcService<CarService>();

app.UseSerilogRequestLogging();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
