using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebAPI.Models;

namespace WebAPI.Endpoints.UserEndpoints.GetInfo;

internal class Endpoint(UserManager<User> userManager) : EndpointWithoutRequest<UserResponse>
{
    private readonly UserManager<User> _userManager = userManager;

    public override void Configure()
    {
        Get("me");
        Group<UserGroup>();
        Description(d => d.WithName("GetCurrentUser").WithDescription("Get the current authenticated user's information"));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var _))
        {
            ThrowError("User not authenticated or invalid user ID");
            return;
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            ThrowError("User not found");
            return;
        }

        await SendAsync(user.ToResponse(), cancellation: ct);
    }
}