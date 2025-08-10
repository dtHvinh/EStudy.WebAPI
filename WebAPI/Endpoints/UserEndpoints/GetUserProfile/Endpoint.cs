using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models._others;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.UserEndpoints.GetUserProfile;

public class Endpoint(ApplicationDbContext context) : Endpoint<GetUserProfileRequest, GetUserProfileResponse>
{
    public override void Configure()
    {
        Get("profile/{UserName}");
        Get("profile");
        AllowAnonymous();
        Description(x => x.WithName("GetUserProfile").WithSummary("Get user profile information."));
        Group<UserGroup>();
    }
    public override async Task HandleAsync(GetUserProfileRequest req, CancellationToken ct)
    {
        User? user;

        if (req.UserName == null)
        {
            user = await context.Users.FindAsync([int.Parse(this.RetrieveUserId())], ct);
        }
        else
        {
            user = await context.Users.FirstOrDefaultAsync(u => u.UserName == req.UserName, ct);
        }

        if (user == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var response = user.MapToResponse();
        await SendOkAsync(response, ct);
    }
}
