namespace API.Middleware;

public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        // Log request start
        logger.LogInformation("Handling request: {Method} {Path}", context.Request.Method, context.Request.Path);

        try
        {
            await next(context); // Call the next middleware in the pipeline

            // Log response status
            logger.LogInformation("Response status: {StatusCode}", context.Response.StatusCode);
        }
        catch (Exception ex)
        {
            // Log the exception
            logger.LogError(ex, "An unhandled exception occurred while processing the request.");

            // Re-throw the exception to be handled by the UseExceptionHandler middleware
            throw;
        }
    }
}
