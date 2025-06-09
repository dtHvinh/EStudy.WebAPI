using Riok.Mapperly.Abstractions;
using WebAPI.Models._flashCard;

namespace WebAPI.Endpoints.FlashCardEndpoints.EditCardFromSet;

[Mapper(AllowNullPropertyAssignment = false)]
public static partial class Mapper
{
    public static partial void ApplyUpdate([MappingTarget] this FlashCard target, EditCardFromSetRequest source);
}
