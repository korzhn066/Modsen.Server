using Modsen.Server.Authentication.Api.Configuration;
using Modsen.Server.Authentication.Application.Services;
using Modsen.Server.Authentication.Domain.Interfaces.Services;
using Modsen.Server.Authentication.Infrastructure.Data;
using Modsen.Server.Authentication.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modsen.Server.Authentication.Application;
using Microsoft.AspNetCore.CookiePolicy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddInfrastructure()
    .AddApplication();

builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityConfiguration();
builder.Services.AddAuthenticateConfiguration(builder);

builder.Services.AddScoped<IApplicationAuthenticateService, ApplicationAuthenticateService>();
builder.Services.AddScoped<ITokenProviderService, TokenProviderService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.None,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
