using Riok.Mapperly.Abstractions;
using WebAPI.Models._flashCard;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.GetSetInfo;

[Mapper]
public static partial class Mapper
{
    public static partial GetSetInfoResponse ToInfoResponse(this FlashCardSet cardSet);
}
