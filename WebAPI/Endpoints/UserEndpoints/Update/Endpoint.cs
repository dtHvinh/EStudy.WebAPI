using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebAPI.Models._others;

namespace WebAPI.Endpoints.UserEndpoints.Update;

internal class Endpoint(UserManager<User> userManager)
    : Endpoint<UpdateUserRequest>
{
    private readonly UserManager<User> _userManager = userManager;

    public override void Configure()
    {
        Put("me");
        Group<UserGroup>();
        Description(d => d.WithName("UpdateCurrentUser").WithDescription("Update the current authenticated user's information"));
    }

    public override async Task HandleAsync(UpdateUserRequest req, CancellationToken ct)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var _))
        {
            ThrowError("Invalid user ID");
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            ThrowError("User not found");
        }

        req.ApplyUpdate(user);

        var updateResult = await _userManager.UpdateAsync(user);

        if (updateResult.Succeeded)
        {
            await SendOkAsync(ct);
        }
        else
        {
            ThrowError("Failed to update user");
        }
    }
}