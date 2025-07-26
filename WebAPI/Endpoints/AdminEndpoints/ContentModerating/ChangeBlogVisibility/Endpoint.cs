using FastEndpoints;
using WebAPI.Data;
using WebAPI.Endpoints.AdminEndpoints.ContentModerating.HideBlog;
using WebAPI.Services.Contract;

namespace WebAPI.Endpoints.AdminEndpoints.ContentModerating.ChangeBlogVisibility;

public class Endpoint(ApplicationDbContext context, ICurrentUserService currentUserService) : Endpoint<ChangeBlogVisibilityRequest>
{
    private readonly ApplicationDbContext _context = context;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public override void Configure()
    {
        Put("content-moderator/blogs/visibility");
        Group<AdminGroup>();
    }
    public override async Task HandleAsync(ChangeBlogVisibilityRequest req, CancellationToken ct)
    {
        if (!await _currentUserService.IsAdminAsync())
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var blog = await _context.Blogs.FindAsync([req.BlogId], ct);

        if (blog is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        blog.IsHidden = !req.IsVisible;
        _context.Blogs.Update(blog);

        await _context.SaveChangesAsync(ct);
        await SendOkAsync(ct);
    }
}
