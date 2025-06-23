using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Endpoints.BlogEndpoints.GetUserBlog;

namespace WebAPI.Endpoints.BlogEndpoints.SearchBlog;

public class Endpoint(ApplicationDbContext context) : Endpoint<GetUserBlogRequest, List<SearchBlogResponse>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("search");
        Group<BlogGroup>();
        Description(d => d.WithName("SearchBlog").WithDescription("Gets blogs including author and pagination"));
    }

    public override async Task HandleAsync(GetUserBlogRequest req, CancellationToken ct)
    {
        var q = Query<string>("q", false);

        var query = _context.Blogs
            .Include(e => e.Author)
            .AsQueryable();

        if (!string.IsNullOrEmpty(q))
            query = query.Where(e => e.SearchVector.Matches(q));

        var result = await query
            .Skip((req.Page - 1) * req.PageSize)
            .Take(req.PageSize)
            .ProjectToSearchResponse()
            .ToListAsync(ct);


        await SendOkAsync(result, ct);
    }
}