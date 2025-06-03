using Riok.Mapperly.Abstractions;
using WebAPI.Models;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.AddCard;

[Mapper]
public static partial class Mapper
{
    public static partial FlashCard ToFlashCard(this AddCardRequest request, int flashCardSetId);
}
