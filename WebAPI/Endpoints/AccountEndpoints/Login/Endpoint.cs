using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using WebAPI.Constants;
using WebAPI.Models._others;
using WebAPI.Services;
using WebAPI.Services.Contract;

namespace WebAPI.Endpoints.AccountEndpoints.Login;

internal class Endpoint(UserManager<User> userManager, IJwtService jwtService, IBanService banService)
    : Endpoint<LoginRequest, LoginResponse>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IJwtService _jwtService = jwtService;
    private readonly IBanService _banService = banService;

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

        if (await _banService.IsUserBannedAsync(user.Id.ToString()))
        {
            var banDueDate = await _banService.GetBanDueDateAsync(user.Id.ToString());
            ThrowError(MessageTemplates.UserIsBanned(banDueDate.Value), StatusCodesV2.Status701AccountBanned);
            return;
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, req.Password);
        if (!isPasswordValid)
            ThrowError("Password is incorrect");

        var token = _jwtService.GenerateToken(user);
        var rt = _jwtService.GenerateRefreshToken();

        user.RefreshToken = rt;

        await _userManager.UpdateAsync(user);

        await SendAsync(new LoginResponse(token, rt), cancellation: ct);
    }
}