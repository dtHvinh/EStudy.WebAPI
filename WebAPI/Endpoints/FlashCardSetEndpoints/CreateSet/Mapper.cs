using Riok.Mapperly.Abstractions;
using WebAPI.Models;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.Create;

[Mapper]
public static partial class Mapper
{
    public static partial FlashCardSet ToFlashCardSet(this CreateFlashCardSetRequest request, string authorId);
    public static partial CreateFlashCardSetResponse ToResponse(this FlashCardSet flashCardSet);
}
