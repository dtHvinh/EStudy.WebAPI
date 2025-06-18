using Riok.Mapperly.Abstractions;
using WebAPI.Models._flashCard;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.GetCardForStudy;

[Mapper]
public static partial class Mapper
{
    public static partial IQueryable<FlashCardStudyResponse> ProjectToStudyResponse(this IQueryable<FlashCard> flashCards);
}
