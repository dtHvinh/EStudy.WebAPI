using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.BlogEndpoints.GetUserBlog;

public class Endpoint(ApplicationDbContext context) : Endpoint<GetUserBlogRequest, List<BlogResponse>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("mine");
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

        var skip = (req.Page - 1) * req.PageSize;

        var blogs = await _context.Blogs
            .Where(b => b.AuthorId == authorId)
            .OrderByDescending(b => b.CreationDate)
            .Skip(skip)
            .Take(req.PageSize)
            .ProjectToResponse()
            .ToListAsync(ct);

        await SendAsync(blogs, cancellation: ct);
    }
}