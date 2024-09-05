using Microsoft.AspNetCore.Builder;
using Modsen.Server.Shared.Middlewares;

namespace Modsen.Server.Shared.MiddlewareExtensions
{
    public static class UseExceptionHandlerMiddlewareExtension
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
