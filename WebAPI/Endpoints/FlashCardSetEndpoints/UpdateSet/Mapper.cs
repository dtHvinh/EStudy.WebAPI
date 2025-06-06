using Riok.Mapperly.Abstractions;
using WebAPI.Endpoints.FlashCardSetEndpoints.UpdateSet;
using WebAPI.Models._flashCard;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.UpdateSetName;

[Mapper]
public static partial class Mapper
{
    [MapperIgnoreTarget(nameof(FlashCardSet.Id))]
    public static partial void UpdateSet([MappingTarget] this FlashCardSet flashCardSet, UpdateSetRequest request);
}
