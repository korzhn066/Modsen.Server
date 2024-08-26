using Modsen.Server.CarsControl.DataAccess;
using Modsen.Server.CarsControl.Business;
using MongoDB.Driver;
using Modsen.Server.CarsControl.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(
    new MongoClient(builder.Configuration["MongoDbSettings:ConnectionString"])
    .GetDatabase(builder.Configuration["MongoDbSettings:DataBaseName"]));

builder.Services
    .AddDataAccess()
    .AddBusiness();

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
