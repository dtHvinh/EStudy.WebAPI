using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.GetUserSet;

public class EndpointV2(ApplicationDbContext context) : Endpoint<GetUserSetRequest, List<FlashCardSetResponse>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("mine");
        Group<FlashCardSetGroup>();
        Description(d => d.WithName("GetUserFlashCardSet").WithDescription("Gets the current user's flashcard sets"));
    }

    public override async Task HandleAsync(GetUserSetRequest req, CancellationToken ct)
    {
        var userId = this.RetrieveUserId();

        if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var authorId))
        {
            ThrowError("User not authenticated or invalid user ID");
            return;
        }

        var skip = (req.Page - 1) * req.PageSize;

        var flashCardSets = await _context.FlashCardSets
            .OrderByDescending(b => b.IsFavorite)
            .ThenByDescending(b => b.Id)
            .Where(b => b.AuthorId == authorId)
            .Skip(skip)
            .Take(req.PageSize)
            .ProjectToResponse()
            .ToListAsync(ct);

        await SendAsync(flashCardSets, cancellation: ct);
    }
}