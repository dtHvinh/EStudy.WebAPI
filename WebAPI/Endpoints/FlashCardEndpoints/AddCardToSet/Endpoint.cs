using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Endpoints.FlashCardSetEndpoints.AddCard;
using WebAPI.Services;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.FlashCardEndpoints.AddCardToSet;

public class Endpoint(ApplicationDbContext context, FileService fileService) : Endpoint<AddCardRequest, AddCardResponse>
{
    private readonly ApplicationDbContext _context = context;
    private readonly FileService _fileService = fileService;

    public override void Configure()
    {
        Post("add-to/{setId}");
        AllowFileUploads();
        Group<FlashCardGroup>();
    }

    public override async Task HandleAsync(AddCardRequest req, CancellationToken ct)
    {
        var cardSet = await _context.FlashCardSets.FirstOrDefaultAsync(e => e.Id == req.SetId, ct);

        if (cardSet is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (!cardSet.IsBelongTo(this.RetrieveUserId()))
            await SendForbiddenAsync(ct);

        var newFlashCard = req.ToFlashCard(req.SetId);

        if (req.Image is not null)
        {
            var imageUrl = await _fileService.UploadFlashCardImage(req.Image, ct);
            newFlashCard.ImageUrl = imageUrl;
        }

        _context.FlashCards.Add(newFlashCard);

        if (await _context.SaveChangesAsync(ct) > 0)
            await SendOkAsync(newFlashCard.ToResponse(), ct);
        else
            ThrowError("Unable to add flash card to set");
    }
}