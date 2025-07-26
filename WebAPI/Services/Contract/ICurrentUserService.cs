
namespace WebAPI.Services.Contract;

public interface ICurrentUserService
{
    int GetId();
    string GetIdAsString();
    Task<bool> IsAdminAsync();
    Task<bool> IsInRoleAsync(string roleName);
}
