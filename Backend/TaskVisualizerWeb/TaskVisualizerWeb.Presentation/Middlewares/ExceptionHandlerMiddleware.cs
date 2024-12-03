using System.Net;
using TaskVisualizerWeb.Domain.Exceptions;

namespace TaskVisualizerWeb.Presentation.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var statusCode = (int)HttpStatusCode.InternalServerError;

                if (ex is ResourceNotFoundException)
                    statusCode = (int)HttpStatusCode.NotFound;

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    context.Response.StatusCode,
                    ex.Message
                };

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}