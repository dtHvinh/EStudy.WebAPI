using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.BlogEndpoints.DeleteBlog;

public class Endpoint(ApplicationDbContext context) : Endpoint<DeleteBlogRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Delete("{id}");
        Group<BlogGroup>();
    }

    public override async Task HandleAsync(DeleteBlogRequest req, CancellationToken ct)
    {
        var blog = await _context.Blogs
            .WhereContentIsValid()
            .Include(e => e.Author)
            .FirstOrDefaultAsync(e => e.Id == req.Id, ct);

        if (blog is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (!blog.IsBelongTo(this.RetrieveUserId()))
        {
            await SendForbiddenAsync(ct);
        }

        _context.Blogs.Remove(blog);

        if (await _context.SaveChangesAsync(ct) == 1)
            await SendNoContentAsync(ct);

        ThrowError("Unable to delete blog");
    }
}
