using FastEndpoints;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.TestEndpoints.SendComment;

public class Endpoint(ApplicationDbContext context)
    : Endpoint<SendCommentRequest, SendCommentResponse>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("{TestId}/comments");
        Group<TestGroup>();
        Description(d => d
            .WithName("Send Comment")
            .WithDescription("Send a comment to a test")
            .Produces<SendCommentResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status500InternalServerError));
    }

    public override async Task HandleAsync(SendCommentRequest req, CancellationToken ct)
    {
        var newComment = req.ToComment(this.RetrieveUserId());

        _context.TestComments.Add(newComment);

        if (await _context.SaveChangesAsync(ct) > 0)
        {
            _context.Entry(newComment).Reference(x => x.Author).Load();
            await SendOkAsync(newComment.ToResponse(), ct);
        }
        else
        {
            ThrowError("Failed to send comment");
        }
    }
}
