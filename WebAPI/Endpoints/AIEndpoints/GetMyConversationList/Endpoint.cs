using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.AIEndpoints.GetMyConversationList;

public class Endpoint(ApplicationDbContext context) : Endpoint<GetMyConversationListRequest, List<GetMyConversationListResponse>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("conversations");
        AllowAnonymous();
        Description(x => x
            .WithName("Get conversations")
            .Produces<int>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest));
        Group<AIGroup>();
    }

    public override async Task HandleAsync(GetMyConversationListRequest req, CancellationToken ct)
    {
        var res = await _context.Conversations
            .AsNoTracking()
            .Where(e => e.UserId == int.Parse(this.RetrieveUserId()))
            .OrderByDescending(x => x.LastActive)
            .ThenByDescending(x => x.CreationDate)
            .Paginate(req.Page, req.PageSize)
            .ProjectToResponse()
            .ToListAsync(ct);

        await SendOkAsync(res, ct);
    }
}
