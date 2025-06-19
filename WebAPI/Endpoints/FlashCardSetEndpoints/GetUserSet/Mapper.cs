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

    public static IQueryable<FlashCardSetResponse> ProjectToResponse(this IQueryable<FlashCardSet> flashCardSets)
    {
        return Queryable.Select(
            flashCardSets,
            static x => new FlashCardSetResponse()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                LastAccess = x.LastAccess,
                IsFavorite = x.IsFavorite,
                CardCount = x.FlashCards.Count,
                Progress = x.FlashCards.Count(e => e.IsSkipped)
            }
        );
    }
}
