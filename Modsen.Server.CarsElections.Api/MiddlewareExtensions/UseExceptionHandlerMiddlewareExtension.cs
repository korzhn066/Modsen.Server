using Modsen.Server.CarsElections.Api.Middlewares;

namespace Modsen.Server.CarsElections.Api.MiddlewareExtensions
{
    public static class UseExceptionHandlerMiddlewareExtension
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
