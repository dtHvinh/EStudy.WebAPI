namespace WebAPI.Endpoints.AccountEndpoints.Register;

public sealed record RegisterRequest(string Name, string UserName, string Password);

public sealed record RegisterResponse(string AccessToken);