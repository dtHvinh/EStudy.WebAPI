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
        Put("{cardId}");
        AllowFileUploads();
        Group<FlashCardGroup>();
        Description(d => d
            .WithName("Edit Flash Card")
            .WithDescription("Edit an existing flashcard with optional image update")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError));
    }

    public override async Task HandleAsync(EditCardFromSetRequest req, CancellationToken ct)
    {
        var card = await _context.FlashCards.Include(e => e.FlashCardSet).ThenInclude(e => e.Author).FirstOrDefaultAsync(e => e.Id == req.CardId, ct);
        if (card is null)
        {
            ThrowError("Card not found");
            return;
        }

        if (!card.FlashCardSet.IsBelongTo(this.RetrieveUserId()))
            ThrowError("You are not the author of this set");

        card.ApplyUpdate(req);
        if (req.Image is not null)
        {
            if (card.ImageUrl is not null)
                await _fileService.UpdateFlashCardImage(card.ImageUrl, req.Image, ct);
            else
                card.ImageUrl = await _fileService.UploadFlashCardImage(req.Image, this.RetrieveUserId(), ct);
        }

        _context.FlashCards.Update(card);

        if (await _context.SaveChangesAsync(ct) == 1)
            await SendNoContentAsync(ct);
        else
            ThrowError("Failed to update card from set");
    }
}
