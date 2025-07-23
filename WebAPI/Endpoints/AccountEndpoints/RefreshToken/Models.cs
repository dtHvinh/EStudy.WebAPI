namespace WebAPI.Endpoints.AccountEndpoints.RefreshToken;

public sealed class RefreshTokenRequest
{
    public string RefreshToken { get; set; } = default!;
}

public sealed class RefreshTokenResponse
{
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}