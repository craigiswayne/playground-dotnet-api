namespace playground_dotnet_api.Middleware;

public class SecurityHeadersMiddleware
{
    private readonly RequestDelegate _next;

    public SecurityHeadersMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Response.Headers.ContainsKey("Content-Security-Policy"))
        {
            context.Response.Headers.Add("Content-Security-Policy", "default-src 'self';");
        }

        if (!context.Response.Headers.ContainsKey("Permissions-Policy"))
        {
            context.Response.Headers.Add("Permissions-Policy", "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()");
        }

        if (!context.Response.Headers.ContainsKey("Referrer-Policy"))
        {
            context.Response.Headers.Add("Referrer-Policy", "same-origin");
        }

        if (!context.Response.Headers.ContainsKey("X-Content-Type-Options"))
        {
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
        }

        if (!context.Response.Headers.ContainsKey("X-Frame-Options"))
        {
            context.Response.Headers.Add("X-Frame-Options", "DENY");
        }

        if (!context.Response.Headers.ContainsKey("X-Permitted-Cross-Domain-Policies"))
        {
            context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");
        }

        if (!context.Response.Headers.ContainsKey("X-Xss-Protection"))
        {
            context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
        }

        await _next(context);
    }
}

public static class SecurityHeadersMiddlewareExtensions
{
    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SecurityHeadersMiddleware>();
    }
}