using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using WebAPI.Models._others;
using WebAPI.Services;

namespace WebAPI.Endpoints.AccountEndpoints.Register;

internal class Endpoint(UserManager<User> userManager, IJwtService jwtService)
    : Endpoint<RegisterRequest, RegisterResponse>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IJwtService _jwtService = jwtService;

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

        await SendOkAsync(new(_jwtService.GenerateToken(newUser)), ct);
    }
}
