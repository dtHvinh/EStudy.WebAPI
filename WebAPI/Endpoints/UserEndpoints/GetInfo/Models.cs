namespace WebAPI.Endpoints.UserEndpoints.GetInfo;

public sealed class UserResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = default!;
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public string? ProfilePicture { get; init; }
    public bool IsOnBoarded { get; init; }
    public DateTimeOffset CreationDate { get; init; }
}
