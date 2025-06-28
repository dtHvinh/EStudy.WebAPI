using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using WebAPI.Models._others;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.UserEndpoints.GetRoles;

public class Endpoint(UserManager<User> userManager)
    : EndpointWithoutRequest<List<string>>
{
    private readonly UserManager<User> _userManager = userManager;

    public override void Configure()
    {
        Get("my-roles");
        Group<UserGroup>();
        Description(d => d.WithName("GetCurrentUserRoles").WithDescription("Get the current authenticated user's roles"));
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = this.RetrieveUserId();

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var res = await _userManager.GetRolesAsync(user);

        await SendOkAsync([.. res], ct);
    }
}
