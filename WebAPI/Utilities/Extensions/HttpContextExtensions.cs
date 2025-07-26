using System.Security.Claims;

namespace WebAPI.Utilities.Extensions;

public static class HttpContextExtensions
{
    public static string? GetId(this HttpContext context)
    {
        return context?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
