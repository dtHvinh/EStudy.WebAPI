namespace WebAPI.Endpoints.AdminEndpoints.UserManagement.EditUserRole;

public sealed class EditUserRoleRequest
{
    public int UserId { get; set; }
    public List<int> RoleIds { get; set; } = default!;
}
