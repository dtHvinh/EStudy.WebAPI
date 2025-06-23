using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.BlogEndpoints.GetDetails;

public class Endpoint(ApplicationDbContext context) : Endpoint<GetBlogDetailsRequest, GetBlogDetailsResponse>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("{id}");
        Group<BlogGroup>();
        Description(d => d.WithName("GetBlogDetails").WithDescription("Gets the blog details"));

    }

    public override async Task HandleAsync(GetBlogDetailsRequest req, CancellationToken ct)
    {
        var requestUserId = this.RetrieveUserId();

        if (string.IsNullOrEmpty(requestUserId) || !int.TryParse(requestUserId, out var userId))
        {
            ThrowError("User not authenticated or invalid user ID");
            return;
        }

        var blog = await _context.Blogs
            .Include(e => e.Author)
            .Where(e => e.Id == req.Id)
            .FirstOrDefaultAsync(ct);

        if (blog is null)
        {
            ThrowError("Blog not found or you don't have permission to view it");
        }

        var isReadonly = blog.Author.Id != userId;
        var response = blog.ToBlogResponse();
        response.SetReadonly(isReadonly);

        await SendOkAsync(response, ct);
    }
}