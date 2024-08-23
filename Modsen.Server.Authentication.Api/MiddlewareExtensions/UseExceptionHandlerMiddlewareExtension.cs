using Modsen.Server.Authentication.Api.Middlewares;

namespace Modsen.Server.Authentication.Api.MiddlewareExtensions
{
    public static class UseExceptionHandlerMiddlewareExtension
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
