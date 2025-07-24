using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Endpoints.AdminEndpoints.UserManagement.WarningUser;

public class Endpoint(ApplicationDbContext context) : Endpoint<WarningUserRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("users/{UserId}/warnings/{Action}");
        Description(x => x.WithTags("Admin").WithSummary("Get users for admin management"));
        Group<AdminGroup>();
    }

    public override async Task HandleAsync(WarningUserRequest req, CancellationToken ct)
    {
        var user = await _context.Users.FirstOrDefaultAsync(e => e.Id == req.UserId, ct);

        if (user is null)
        {
            ThrowError("User not found", StatusCodes.Status404NotFound);
            return;
        }

        user.WarningCount = req.Action == "increment" ? user.WarningCount + 1 :
                                req.Action == "clear" ? 0 : throw new InvalidOperationException("Action not recognized");

        _context.Users.Update(user);
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}
