using System.Net;

namespace TaskVisualizerWeb.Presentation.Middlewares
{
    public class InvalidDataExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public InvalidDataExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Call the next middleware in the pipeline
            }
            catch (InvalidDataException ex)
            {
                // Handle InvalidDataException
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
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