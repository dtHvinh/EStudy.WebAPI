using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Endpoints.AdminEndpoints.UserManagement.EditUserRole;

public class Endpoint(ApplicationDbContext context) : Endpoint<EditUserRoleRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Put("users/{userId}/roles");
        Description(x => x.WithTags("Admin").WithSummary("Edit user roles for admin management"));
        Group<AdminGroup>();
    }
    public override async Task HandleAsync(EditUserRoleRequest req, CancellationToken ct)
    {
        var user = await _context.Users.FindAsync([req.UserId], ct);
        if (user == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        var existingRoleIds = await _context.UserRoles
            .Where(ur => ur.UserId == req.UserId)
            .Select(e => e.RoleId)
            .ToListAsync(ct);

        if (req.RoleIds.Count > existingRoleIds.Count)
        {
            var newRoleIds = req.RoleIds.Except(existingRoleIds).ToList();
            foreach (var roleId in newRoleIds)
            {
                _context.UserRoles.Add(new IdentityUserRole<int> { UserId = req.UserId, RoleId = roleId });
            }
        }
        else if (req.RoleIds.Count < existingRoleIds.Count)
        {
            var removedRoleIds = existingRoleIds.Except(req.RoleIds).ToList();
            foreach (var roleId in removedRoleIds)
            {
                var userRole = await _context.UserRoles
                    .FirstOrDefaultAsync(ur => ur.UserId == req.UserId && ur.RoleId == roleId, ct);
                if (userRole != null)
                {
                    _context.UserRoles.Remove(userRole);
                }
            }
        }

        await _context.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}
