using Riok.Mapperly.Abstractions;
using WebAPI.Models._others;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.GetUserSet;

[Mapper]
public static partial class Mapper
{
    public static partial FlashCardSetResponse ToResponse(this FlashCardSet flashCardSet);
    public static partial IQueryable<FlashCardSetResponse> ProjectToResponse(this IQueryable<FlashCardSet> flashCardSets);
}
