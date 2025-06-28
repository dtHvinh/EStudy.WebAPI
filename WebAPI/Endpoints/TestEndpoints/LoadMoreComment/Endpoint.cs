using FastEndpoints;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.TestEndpoints.LoadMoreComment;

public class Endpoint(ApplicationDbContext context)
    : Endpoint<LoadMoreCommentRequest, List<LoadMoreCommentResponse>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("{TestId}/comments");
        Group<TestGroup>();
        Description(d => d.WithName("Load comments").WithDescription("Load comments"));

    }

    public override async Task HandleAsync(LoadMoreCommentRequest req, CancellationToken ct)
    {
        var res = _context.TestComments
            .Where(e => e.TestId == req.TestId)
            .OrderByDescending(e => e.CreationDate)
            .Skip((req.Page - 1) * req.PageSize)
            .Take(req.PageSize)
            .ProjectToResponse(this.RetrieveUserId())
            .ToList();

        await SendOkAsync(res, ct);
    }
}