namespace WebAPI.Endpoints.UserEndpoints.Update;


public sealed class UpdateUserRequest
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}
