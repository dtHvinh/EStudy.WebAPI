using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using WebAPI.Models._others;
using WebAPI.Services;

namespace WebAPI.Endpoints.AccountEndpoints.Login;

internal class Endpoint(UserManager<User> userManager, IJwtService jwtService)
    : Endpoint<LoginRequest, LoginResponse>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IJwtService _jwtService = jwtService;

    public override void Configure()
    {
        Post("login");
        AllowAnonymous();
        Group<AccountGroup>();
        Description(d => d.WithName("LoginAccount").WithDescription("Login account"));
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var user = await _userManager.FindByNameAsync(req.UserNameOrEmail)
            ?? await _userManager.FindByEmailAsync(req.UserNameOrEmail);

        if (user == null)
            ThrowError("Invalid username or email");

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, req.Password);

        if (!isPasswordValid)
            ThrowError("Password is incorrect");

        var token = _jwtService.GenerateToken(user);

        await SendAsync(new LoginResponse(token), cancellation: ct);
    }
}