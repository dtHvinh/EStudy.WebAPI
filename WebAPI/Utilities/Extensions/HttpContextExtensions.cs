using System.Security.Claims;

namespace WebAPI.Utilities.Extensions;

public static class HttpContextExtensions
{
    public static string? GetId(this HttpContext context)
    {
        return context?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public static void StartStreaming(this HttpContext context)
    {
        context.Response.ContentType = "text/event-stream";
        context.Response.Headers.Append("Cache-Control", "no-cache, no-transform, private");
        context.Response.Headers.Append("Connection", "keep-alive");
    }

    public static async Task SendMessageAsync(this HttpContext context, string message, CancellationToken ct)
    {
        await context.Response.WriteAsync(message.ToWellFormStreamResponse(), ct);
        await context.Response.Body.FlushAsync(ct);
    }
}
