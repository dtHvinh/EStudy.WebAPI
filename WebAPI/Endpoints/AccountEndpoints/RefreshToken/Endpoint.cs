using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models._others;
using WebAPI.Services;

namespace WebAPI.Endpoints.AccountEndpoints.RefreshToken;

public class Endpoint(UserManager<User> userManager, IJwtService jwtService) : Endpoint<RefreshTokenRequest, RefreshTokenResponse>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IJwtService _jwtService = jwtService;

    public override void Configure()
    {
        Post("refresh-token");
        AllowAnonymous();
        Group<AccountGroup>();
    }
    public override async Task HandleAsync(RefreshTokenRequest req, CancellationToken ct)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(e => e.RefreshToken == req.RefreshToken, ct);
        if (user is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (user.RefreshToken != req.RefreshToken)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var token = _jwtService.GenerateToken(user);
        var refreshToken = _jwtService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        await _userManager.UpdateAsync(user);

        await SendOkAsync(new RefreshTokenResponse()
        {
            AccessToken = token,
            RefreshToken = refreshToken
        }, ct);
    }
}
