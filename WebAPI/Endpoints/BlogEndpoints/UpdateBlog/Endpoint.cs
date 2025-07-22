using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.BlogEndpoints.UpdateBlog;

public class Endpoint(ApplicationDbContext context) : Endpoint<UpdateBlogRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Put("{id}");
        Group<BlogGroup>();
    }

    public override async Task HandleAsync(UpdateBlogRequest req, CancellationToken ct)
    {
        var blog = await _context.Blogs
            .WhereContentIsValid()
            .Include(e => e.Author)
            .FirstOrDefaultAsync(e => e.Id == req.Id, cancellationToken: ct);

        if (blog is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (blog.AuthorId != int.Parse(this.RetrieveUserId()))
        {
            await SendForbiddenAsync(ct);
            return;
        }

        blog.UpdateFrom(req);

        _context.Blogs.Update(blog);

        if (await _context.SaveChangesAsync(ct) == 1)
        {
            await SendNoContentAsync(ct);
        }
        else
        {
            ThrowError("Failed to update blog");
        }
    }
}
