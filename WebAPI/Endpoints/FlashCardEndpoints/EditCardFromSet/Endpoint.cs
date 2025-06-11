using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Services;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.FlashCardEndpoints.EditCardFromSet;

public class Endpoint(ApplicationDbContext context, FileService fileService) : Endpoint<EditCardFromSetRequest, EditCardFromSetResponse>
{
    private readonly ApplicationDbContext _context = context;
    private readonly FileService _fileService = fileService;

    public override void Configure()
    {
        Put("{cardId}/from-set/{setId}");
        AllowFileUploads();
        Group<FlashCardGroup>();
    }

    public override async Task HandleAsync(EditCardFromSetRequest req, CancellationToken ct)
    {
        var set = await _context.FlashCardSets.Include(e => e.Author).FirstOrDefaultAsync(e => e.Id == req.SetId, ct);
        if (set is null)
            ThrowError("Set not found");
        else if (!set.IsBelongTo(this.RetrieveUserId()))
            ThrowError("You are not the author of this set");

        var card = await _context.FlashCards.FindAsync([req.CardId], ct);
        if (card is null)
        {
            ThrowError("Card not found");
            return;
        }

        card.ApplyUpdate(req);
        if (req.Image is not null)
        {
            if (card.ImageUrl is not null)
                await _fileService.UpdateFlashCardImage(card.ImageUrl, req.Image, ct);
            else
                await _fileService.UploadFlashCardImage(req.Image, ct);
        }
        else if (card.ImageUrl is not null)
            await _fileService.DeleteFlashCardImage(card.ImageUrl, ct);

        _context.FlashCards.Update(card);

        if (await _context.SaveChangesAsync(ct) == 1)
            await SendNoContentAsync(ct);
        else
            ThrowError("Failed to update card from set");
    }
}
