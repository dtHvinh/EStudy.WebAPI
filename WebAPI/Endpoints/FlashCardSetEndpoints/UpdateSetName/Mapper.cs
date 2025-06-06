using Riok.Mapperly.Abstractions;
using WebAPI.Models._flashCard;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.UpdateSetName;

[Mapper]
public static partial class Mapper
{
    [MapperIgnoreTarget(nameof(FlashCardSet.Id))]
    public static partial void UpdateSetName([MappingTarget] this FlashCardSet flashCardSet, UpdateSetNameRequest request);
}
