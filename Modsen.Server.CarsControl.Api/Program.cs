using Modsen.Server.CarsControl.DataAccess;
using Modsen.Server.CarsControl.Business;
using Modsen.Server.CarsControl.Api;
using Modsen.Server.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresintation()
    .AddDataAccess(builder)
    .AddBusiness()
    .AddShared();

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
