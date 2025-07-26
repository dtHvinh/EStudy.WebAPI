using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Services.Contract;
using WebAPI.Utilities.Extensions;
using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.AdminEndpoints.UserManagement.GetUsers;

public class Endpoint(ApplicationDbContext context, IBanService banService) : Endpoint<AdminGetUsersRequest, PagedResponse<AdminGetUserResponse>>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IBanService _banService = banService;

    public override void Configure()
    {
        Get("users");
        Description(x => x.WithTags("Admin").WithSummary("Get users for admin management"));
        Group<AdminGroup>();
    }
    public override async Task HandleAsync(AdminGetUsersRequest req, CancellationToken ct)
    {
        var q = _context.Users.AsQueryable();
        if (!string.IsNullOrWhiteSpace(req.Name))
        {
            q = q.Where(u => u.SearchVector.Matches(EF.Functions.ToTsQuery(req.Name)));
        }
        if (!string.IsNullOrWhiteSpace(req.Role))
        {
            var roleId = int.Parse(req.Role);
            if (roleId > 0)
            {
                q = q.Where(u => _context.UserRoles.Any(ur => ur.UserId == u.Id && ur.RoleId == roleId));
            }
        }

        var users = await q
                    .OrderBy(e => e.Name)
                    .Select(u => new AdminGetUserResponse
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Roles = Queryable.Join(
                                            _context.UserRoles.Where(e => e.UserId == u.Id),
                                            _context.Roles,
                                            l => l.RoleId, r => r.Id,
                                            (l, r) => new UserRoleObject
                                            {
                                                Id = r.Id,
                                                Name = r.Name!
                                            }).ToList(),
                        CreationDate = u.CreationDate,
                        WarningCount = u.WarningCount,
                        Email = u.Email!,
                        ProfilePicture = u.ProfilePicture!,
                        Status = _banService.IsUserBannedAsync(u.Id.ToString()).Result ? "Banned" : "Active"
                    })
                    .Paginate(req.Page, req.PageSize)
                    .ToListAsync(ct);

        var totalCount = await _context.Users.CountAsync(ct);
        var totalPages = (int)Math.Ceiling((double)totalCount / req.PageSize);

        var response = new PagedResponse<AdminGetUserResponse>
        {
            Items = users,
            Page = req.Page,
            PageSize = req.PageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };

        await SendOkAsync(response, ct);
    }
}
