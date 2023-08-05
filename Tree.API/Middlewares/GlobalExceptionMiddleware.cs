using System.Net;
using System.Text.Json;

namespace Service1.API.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<GlobalExceptionMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new
            {
                message = ex.Message
            };

            var errorJson = JsonSerializer.Serialize(errorResponse);

            await response.WriteAsync(errorJson);
        }
    }
}