using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Services;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.FlashCardEndpoints.DeleteCardFromSet;

public class Endpoint(ApplicationDbContext context, FileService fileService) : Endpoint<DeleteCardFromSetRequest>
{
    private readonly ApplicationDbContext _context = context;
    private readonly FileService _fileService = fileService;

    public override void Configure()
    {
        Delete("{cardId}");
        Group<FlashCardGroup>();
    }

    public override async Task HandleAsync(DeleteCardFromSetRequest req, CancellationToken ct)
    {
        var card = await _context.FlashCards.Include(e => e.FlashCardSet).ThenInclude(e => e.Author).FirstOrDefaultAsync(e => e.Id == req.CardId, ct);

        if (card is null)
        {
            ThrowError("Card not found");
            return;
        }

        if (!card.FlashCardSet.IsBelongTo(this.RetrieveUserId()))
        {
            ThrowError("You are not the author of this set");
        }

        _context.FlashCards.Remove(card);
        if (!string.IsNullOrEmpty(card.ImageUrl))
            await _fileService.DeleteFlashCardImage(card.ImageUrl, ct);

        if (await _context.SaveChangesAsync(ct) == 1)
        {
            await SendNoContentAsync(ct);
        }
        else
        {
            ThrowError("Failed to remove card from set");
        }
    }
}
