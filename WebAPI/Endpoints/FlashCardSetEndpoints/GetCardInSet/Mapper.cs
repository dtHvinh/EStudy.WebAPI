using Riok.Mapperly.Abstractions;
using WebAPI.Models._flashCard;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.GetCardInSet;

[Mapper]
public static partial class Mapper
{
    public static partial FlashCardResponse ToResponse(this FlashCard flashCard);
    public static partial IQueryable<FlashCardResponse> ProjectToResponse(this IQueryable<FlashCard> flashCards);
    public static partial ICollection<FlashCardResponse> ProjectToResponse(this ICollection<FlashCard> flashCards);
}
