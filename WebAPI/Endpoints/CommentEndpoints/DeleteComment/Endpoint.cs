using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.CommentEndpoints.DeleteComment;

public class Endpoint(ApplicationDbContext context)
    : Endpoint<DeleteCommentRequest, DeleteCommentResponse>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Delete("{CommentId}");
        Group<CommentGroup>();
        Description(d => d.WithName("Delete comment").WithDescription("Delete specific comment"));

    }

    public override async Task HandleAsync(DeleteCommentRequest req, CancellationToken ct)
    {
        var comment = await _context.TestComments.Include(e => e.Author).FirstOrDefaultAsync(e => e.Id == req.CommentId, ct);

        if (comment is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (comment.AuthorId != int.Parse(this.RetrieveUserId()))
        {
            await SendForbiddenAsync(ct);
            return;
        }

        _context.TestComments.Remove(comment);

        if (await _context.SaveChangesAsync(ct) > 0)
        {
            await SendOkAsync(new DeleteCommentResponse()
            {
                CommentId = req.CommentId,
            }, ct);
        }
        else
        {
            ThrowError("Failed to delete comment");
        }
    }
}
