using Modsen.Server.CarsElections.Api;
using Modsen.Server.CarsElections.Api.MiddlewareExtensions;
using Modsen.Server.CarsElections.Api.Services;
using Modsen.Server.CarsElections.Application;
using Modsen.Server.CarsElections.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddInfrastructure(builder)
    .AddApplication();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
