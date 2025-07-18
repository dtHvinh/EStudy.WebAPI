using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using WebAPI.Models._others;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.UserEndpoints.GetInfo;

internal class Endpoint(UserManager<User> userManager) : EndpointWithoutRequest<UserResponse>
{
    private readonly UserManager<User> _userManager = userManager;

    public override void Configure()
    {
        Get("me");
        Group<UserGroup>();
        Description(d => d
            .WithName("Get Current User")
            .WithDescription("Get the current authenticated user's information")
            .Produces<UserResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = this.RetrieveUserId();

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