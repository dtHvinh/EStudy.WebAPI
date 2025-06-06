using Riok.Mapperly.Abstractions;
using WebAPI.Endpoints.FlashCardSetEndpoints.CreateSet;
using WebAPI.Models._flashCard;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.Create;

[Mapper]
public static partial class Mapper
{
    public static partial FlashCardSet ToFlashCardSet(this CreateFlashCardSetRequest request, string authorId);
    public static partial CreateFlashCardSetResponse ToResponse(this FlashCardSet flashCardSet);
}
