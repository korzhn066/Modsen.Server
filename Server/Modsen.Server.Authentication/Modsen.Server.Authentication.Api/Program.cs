using Modsen.Server.Authentication.Infrastructure;
using Modsen.Server.Authentication.Application;
using Microsoft.AspNetCore.CookiePolicy;
using Modsen.Server.Authentication.Api;
using Modsen.Server.Shared.MiddlewareExtensions;
using Modsen.Server.Shared;
using Modsen.Server.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .ConfigureLogger(builder.Configuration["ELK:LogstashUri"]!);

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

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.None,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseExceptionHandlerMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
