using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;
using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.GetUserSet;

[Obsolete("Use EndpointV2")]
public class EndpointV1(ApplicationDbContext context) : Endpoint<GetUserSetRequest, PagedResponse<FlashCardSetResponse>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("mine");
        Group<FlashCardSetGroup>();
        Description(d => d.WithName("GetUserFlashCardSet").WithDescription("Gets the current user's flashcard sets with pagination"));
    }

    public override async Task HandleAsync(GetUserSetRequest req, CancellationToken ct)
    {
        var userId = this.RetrieveUserId();

        if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var authorId))
        {
            ThrowError("User not authenticated or invalid user ID");
            return;
        }

        var totalCount = await _context.FlashCardSets
            .Where(b => b.AuthorId == authorId)
            .CountAsync(ct);

        var skip = (req.Page.EnsureLargerOrEqual(1) - 1) * req.PageSize.EnsureInRange(1, 50);

        var flashCardSets = await _context.FlashCardSets
            .OrderByDescending(b => b.IsFavorite)
            .ThenByDescending(b => b.Id)
            .Where(b => b.AuthorId == authorId)
            .Include(b => b.FlashCards)
            .Skip(skip)
            .Take(req.PageSize)
            .ProjectToResponse()
            .ToListAsync(ct);

        var response = flashCardSets.ToPagedResponse(req.Page, req.PageSize, totalCount);

        await SendAsync(response, cancellation: ct);
    }
}