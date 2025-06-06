﻿using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.AddCard;

public class Endpoint(ApplicationDbContext context) : Endpoint<AddCardRequest, AddCardResponse>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("{setId}/add");
        Group<FlashCardSetGroup>();
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

        _context.FlashCards.Add(newFlashCard);

        if (await _context.SaveChangesAsync(ct) > 0)
            await SendOkAsync(new AddCardResponse
            {
                Id = newFlashCard.Id
            }, ct);

        ThrowError("Unable to add flash card to set");
    }
}