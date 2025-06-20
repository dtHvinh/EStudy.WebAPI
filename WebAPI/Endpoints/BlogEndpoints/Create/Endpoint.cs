using FastEndpoints;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.BlogEndpoints.Create;

public class Endpoint(ApplicationDbContext context) : Endpoint<CreateBlogRequest, CreateBlogResponse>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("");
        Group<BlogGroup>();
    }

    public override async Task HandleAsync(CreateBlogRequest req, CancellationToken ct)
    {
        var newBlog = req.ToBlog(this.RetrieveUserId());

        _context.Blogs.Add(newBlog);

        if (await _context.SaveChangesAsync(ct) == 1)
            await SendOkAsync(newBlog.ToCreateBlogResponse(), ct);

        ThrowError("Unable to create blog");

    }
}
