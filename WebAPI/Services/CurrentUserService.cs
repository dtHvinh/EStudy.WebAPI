using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebAPI.Models._others;
using WebAPI.Utilities.Attributes;

namespace WebAPI.Services;

[Service(ServiceLifetime.Transient)]
public class CurrentUserService(UserManager<User> userManager, IHttpContextAccessor context)
{
    private readonly UserManager<User> _userManager = userManager;
    public string? Id => context.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

    private string GetIdInternal()
    {
        var userId = context.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return userId ?? throw new UnauthorizedAccessException("User is not authenticated.");
    }
    private static User CreateDummy(string userId)
    {
        return new User
        {
            Id = int.Parse(userId),
            Name = "dummyUser",
        };
    }

    public int GetId()
    {
        var userId = GetIdInternal();
        return int.Parse(userId);
    }

    public async Task<bool> IsInRoleAsync(string roleName)
    {
        var userId = GetIdInternal();
        return await _userManager.IsInRoleAsync(
               CreateDummy(userId), roleName);
    }

    public async Task<bool> IsAdminAsync()
    {
        return await IsInRoleAsync("Admin");
    }
}
