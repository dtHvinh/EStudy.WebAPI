using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using WebAPI.Models;

namespace WebAPI.Endpoints.AccountEndpoints.Register;

internal class Endpoint(UserManager<User> userManager)
    : Endpoint<RegisterRequest, RegisterResponse>
{
    private readonly UserManager<User> _userManager = userManager;

    public override void Configure()
    {
        Post("");
        AllowAnonymous();
        Group<AccountGroup>();
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        User newUser = req.ToUser();

        var createUserResult = await _userManager.CreateAsync(newUser, req.Password);

        if (!createUserResult.Succeeded)
            ThrowError("Failed to create user");

        var addRoleResult = await _userManager.AddToRoleAsync(newUser, R.Student);

        if (!addRoleResult.Succeeded)
            ThrowError("Failed to add role to user");

        await SendOkAsync(ct);
    }
}
