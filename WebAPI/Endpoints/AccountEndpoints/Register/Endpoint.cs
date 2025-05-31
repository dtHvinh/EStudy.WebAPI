using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using WebAPI.Constants;
using WebAPI.Models;

namespace WebAPI.Endpoints.AccountEndpoints.Register;

internal class Endpoint(UserManager<User> userManager)
    : Endpoint<RegisterRequest, RegisterResponse>
{
    private readonly UserManager<User> _userManager = userManager;

    public override void Configure()
    {
        Post("register");
        AllowAnonymous();
        Group<AccountGroup>();
        Description(d => d.WithName("RegisterAccount").WithDescription("Register a new account"));
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        User newUser = req.ToUser();

        var createUserResult = await _userManager.CreateAsync(newUser, req.Password);

        if (!createUserResult.Succeeded)
            ThrowError(createUserResult.Errors.FirstOrDefault()?.Description ?? "Failed to create user");

        var addRoleResult = await _userManager.AddToRoleAsync(newUser, R.Student);

        if (!addRoleResult.Succeeded)
            ThrowError("Failed to add role to user");

        await SendOkAsync(ct);
    }
}
