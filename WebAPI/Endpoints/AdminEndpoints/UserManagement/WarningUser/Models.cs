namespace WebAPI.Endpoints.AdminEndpoints.UserManagement.WarningUser;

public sealed class WarningUserRequest
{
    public int UserId { get; set; }
    public string Action { get; set; } = default!;
}
