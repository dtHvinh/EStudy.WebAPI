using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using WebAPI.Constants;
using WebAPI.Models._others;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.UserEndpoints.OnBoarding;

public class Endpoint(UserManager<User> userManager)
    : Endpoint<OnBoardingRequest>
{
    private readonly UserManager<User> _userManager = userManager;

    public override void Configure()
    {
        Patch("onboarding");
        Group<UserGroup>();
    }

    public override async Task HandleAsync(OnBoardingRequest req, CancellationToken ct)
    {
        var user = await _userManager.FindByIdAsync(this.RetrieveUserId());

        if (user == null)
        {
            ThrowError("User not found");
            return;
        }

        if (req.Role.Equals(R.Instructor, StringComparison.OrdinalIgnoreCase))
            await _userManager.AddToRoleAsync(user, req.Role);

        user.IsOnBoarded = true;
        await _userManager.UpdateAsync(user);

        await SendOkAsync(ct);
    }
}