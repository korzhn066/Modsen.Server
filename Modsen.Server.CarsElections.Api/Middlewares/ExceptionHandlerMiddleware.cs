using Modsen.Server.CarsElections.Domain.Exceptions;
using Newtonsoft.Json;

namespace Modsen.Server.CarsElections.Api.Middlewares
{
    public class ExceptionHandlerMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionMessageAsync(context, ex).ConfigureAwait(false);
            }
        }

        private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {
            var statusCode = GetStatusCodeByException(exception);

            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                ErrorMessage = exception.Message
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(result);
        }

        private static int GetStatusCodeByException(Exception exception)
        {
            return exception switch
            {
                BadRequestException => 400,
                NotFoundException => 404,
                _ => 500
            };
        }
    }
}
