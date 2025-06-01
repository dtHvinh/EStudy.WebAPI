using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;
using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.BlogEndpoints.GetUserBlog;

public class Endpoint(ApplicationDbContext context) : Endpoint<GetUserBlogRequest, PagedResponse<BlogResponse>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("my-blogs");
        Group<BlogGroup>();
        Description(d => d.WithName("GetUserBlogs").WithDescription("Gets the current user's blogs with pagination"));
    }

    public override async Task HandleAsync(GetUserBlogRequest req, CancellationToken ct)
    {
        var userId = this.RetrieveUserId();

        if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var authorId))
        {
            ThrowError("User not authenticated or invalid user ID");
            return;
        }

        var totalCount = await _context.Blogs
            .Where(b => b.AuthorId == authorId)
            .CountAsync(ct);

        var skip = (req.Page.EnsureLargerOrEqual(1) - 1) * req.PageSize.EnsureInRange(1, 50);

        var blogs = await _context.Blogs
            .Where(b => b.AuthorId == authorId)
            .OrderByDescending(b => b.CreationDate)
            .Skip(skip)
            .Take(req.PageSize)
            .Select(b => b.ToBlogResponse())
            .ToListAsync(ct);

        var response = blogs.ToPagedResponse(req.Page, req.PageSize, totalCount);

        await SendAsync(response, cancellation: ct);
    }
}