using Riok.Mapperly.Abstractions;
using WebAPI.Models._flashCard;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.GetUserSet;

[Mapper]
public static partial class Mapper
{
    [MapProperty(
        nameof(FlashCardSet.FlashCards) + "." + nameof(ICollection<FlashCard>.Count),
        nameof(FlashCardSetResponse.CardCount))]
    public static partial FlashCardSetResponse ToResponse(this FlashCardSet flashCardSet);

    [MapProperty(
        nameof(FlashCardSet.FlashCards) + "." + nameof(ICollection<FlashCard>.Count),
        nameof(FlashCardSetResponse.CardCount))]
    public static partial IQueryable<FlashCardSetResponse> ProjectToResponse(this IQueryable<FlashCardSet> flashCardSets);
}
