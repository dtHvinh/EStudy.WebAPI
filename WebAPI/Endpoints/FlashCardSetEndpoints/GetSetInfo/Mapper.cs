using Riok.Mapperly.Abstractions;
using WebAPI.Models._flashCard;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.GetSetInfo;


[Mapper]
public static partial class Mapper
{
    [MapProperty(
        nameof(FlashCardSet.FlashCards) + "." + nameof(ICollection<FlashCard>.Count),
        nameof(GetSetInfoResponse.CardCount))]
    public static partial GetSetInfoResponse ToResponse(this FlashCardSet flashCardSet);

    public static IQueryable<GetSetInfoResponse> ProjectInfoResponse(this IQueryable<FlashCardSet> flashCardSets)
    {
        return Queryable.Select(
            flashCardSets,
            x => new GetSetInfoResponse()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                LastAccess = x.LastAccess,
                CardCount = x.FlashCards.Count,
                Progress = x.FlashCards.Count(e => e.IsSkipped)
            }
        );
    }
}

