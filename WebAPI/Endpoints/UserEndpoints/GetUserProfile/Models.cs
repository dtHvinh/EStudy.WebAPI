namespace WebAPI.Endpoints.UserEndpoints.GetUserProfile;

public sealed class GetUserProfileRequest
{
    public string? UserName { get; set; }
}

public sealed class GetUserProfileResponse
{
    public int Id { get; set; }
    public string UserName { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string ProfilePicture { get; set; } = default!;
    public string? Bio { get; set; } = default!;
}