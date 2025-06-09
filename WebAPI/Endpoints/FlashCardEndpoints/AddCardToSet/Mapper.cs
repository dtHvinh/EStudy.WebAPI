using Riok.Mapperly.Abstractions;
using WebAPI.Endpoints.FlashCardEndpoints.AddCardToSet;
using WebAPI.Models._flashCard;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.AddCard;

[Mapper]
public static partial class Mapper
{
    public static partial FlashCard ToFlashCard(this AddCardRequest request, int flashCardSetId);
    public static partial AddCardResponse ToResponse(this FlashCard card);
}
