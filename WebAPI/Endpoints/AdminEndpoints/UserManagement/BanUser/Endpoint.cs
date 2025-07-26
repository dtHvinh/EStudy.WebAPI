using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models._others;
using WebAPI.Services.Contract;

namespace WebAPI.Endpoints.AdminEndpoints.UserManagement.BanUser;

public class Endpoint(ApplicationDbContext context, IBanService banService, UserManager<User> userManager, ICurrentUserService currentUserService) : Endpoint<BanUserRequest>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IBanService _banService = banService;
    private readonly UserManager<User> _userManager = userManager;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public override void Configure()
    {
        Post("users/{UserId}/{Action:regex(ban|unban)}");
        Description(x => x.WithTags("Admin").WithSummary("Ban user"));
        Group<AdminGroup>();
    }

    public override async Task HandleAsync(BanUserRequest req, CancellationToken ct)
    {
        var user = await _context.Users.FirstOrDefaultAsync(e => e.Id == req.UserId, ct);
        if (user is null)
        {
            ThrowError("User not found", StatusCodes.Status404NotFound);
            return;
        }

        if (!await IsAllowToBanAsync(user))
        {
            return;
        }

        if (req.Action == "unban")
        {
            if (!await _banService.IsUserBannedAsync(user.Id.ToString()))
            {
                ThrowError("User is not banned", StatusCodes.Status400BadRequest);
                return;
            }
            await _banService.UnbanUserAsync(user.Id.ToString());
        }
        else if (req.Action == "ban")
        {
            if (await _banService.IsUserBannedAsync(user.Id.ToString()))
            {
                ThrowError("User is already banned", StatusCodes.Status400BadRequest);
                return;
            }
            var expiry = TimeSpan.FromDays(req.Days);
            await _banService.BanUserAsync(user.Id.ToString(), expiry);
        }
        else
        {
            ThrowError("Invalid action", StatusCodes.Status400BadRequest);
            return;
        }

        await SendOkAsync(ct);
    }

    private async Task<bool> IsAllowToBanAsync(User user)
    {
        if (!await _currentUserService.IsAdminAsync())
        {
            ThrowError("No permission", StatusCodes.Status403Forbidden);
            return false;
        }

        if (user.Id == _currentUserService.GetId())
        {
            ThrowError("Cannot ban yourself", StatusCodes.Status403Forbidden);
            return false;
        }

        if (await _userManager.IsInRoleAsync(user, "Admin"))
        {
            ThrowError("Cannot ban an admin user", StatusCodes.Status403Forbidden);
            return false;
        }

        return true;
    }
}
