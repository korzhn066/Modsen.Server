using Modsen.Server.CarsElections.Api;
using Modsen.Server.CarsElections.Api.Mapper;
using Modsen.Server.CarsElections.Api.MiddlewareExtensions;
using Modsen.Server.CarsElections.Application;
using Modsen.Server.CarsElections.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddInfrastructure(builder)
    .AddApplication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandlerMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
