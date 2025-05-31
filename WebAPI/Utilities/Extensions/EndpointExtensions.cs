using FastEndpoints;
using System.Security.Claims;

namespace WebAPI.Utilities.Extensions;

public static class EndpointExtensions
{
    /// <summary>
    /// Retrieves the authenticated user id from the claims
    /// </summary>
    public static string RetrieveUserId(this BaseEndpoint endpoint)
    {
        return endpoint.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException();
    }
}
