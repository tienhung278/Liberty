using Service1.API.Middlewares;

namespace Service1.API.Extensions;

public static class GlobalExceptionExtension
{
    public static IApplicationBuilder UseGlobalException(this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionMiddleware>();
    }
}