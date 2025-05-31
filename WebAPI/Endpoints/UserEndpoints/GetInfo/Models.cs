namespace WebAPI.Endpoints.UserEndpoints.GetInfo;

public sealed record UserResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = default!;
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
}
