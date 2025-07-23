using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using System.Collections.Specialized;
using System.Text;
using WebAPI.Endpoints.AccountEndpoints.Login;
using WebAPI.Models._others;
using WebAPI.Services;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.AccountEndpoints.LoginWithGoogle;

public class Endpoints(UserManager<User> userManager, IJwtService jwtService, IHttpClientFactory httpClientFactory)
    : EndpointWithoutRequest<LoginResponse>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IJwtService _jwtService = jwtService;
    private readonly IHttpClientFactory _clientFactory = httpClientFactory;

    public override void Configure()
    {
        Post("google-login");
        AllowAnonymous();
        Group<AccountGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        string googleAccessToken = Query<string>("access_token")!;

        NameValueCollection queryString = new()
        {
            { "access_token", googleAccessToken }
        };

        using HttpClient client = _clientFactory.CreateClient("GoogleClient");

        Uri uri = new(new StringBuilder("/oauth2/v3/userinfo").Append(queryString.ToQueryString()).ToString(), UriKind.Relative);

        var response = await client.GetFromJsonAsync<GoogleUserInfoResponse>(uri, ct);

        if (response is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var user = await _userManager.FindByEmailAsync(response.Email);

        if (user is null)
        {
            var (newUser, token) = await RegisterAndReturnToken(response);

            var rt = _jwtService.GenerateRefreshToken();
            newUser.RefreshToken = rt;

            await _userManager.UpdateAsync(newUser);

            await SendOkAsync(new LoginResponse(token, rt), ct);
        }
        else
        {
            var token = _jwtService.GenerateToken(user);

            var rt = _jwtService.GenerateRefreshToken();
            user.RefreshToken = rt;

            await _userManager.UpdateAsync(user);

            await SendOkAsync(new LoginResponse(token, rt), ct);
        }
    }

    private async Task<(User, string)> RegisterAndReturnToken(GoogleUserInfoResponse response)
    {
        var user = response.ToUser();

        var createUserResult = await _userManager.CreateAsync(user);
        if (!createUserResult.Succeeded)
            ThrowError(createUserResult.Errors.FirstOrDefault()?.Description ?? "Failed to create user");
        var token = _jwtService.GenerateToken(user);
        return (user, token);
    }
}