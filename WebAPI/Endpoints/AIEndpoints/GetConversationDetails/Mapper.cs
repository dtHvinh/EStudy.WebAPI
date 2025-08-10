using Riok.Mapperly.Abstractions;
using WebAPI.Models._ai;

namespace WebAPI.Endpoints.AIEndpoints.GetConversationDetails;

[Mapper]
public static partial class Mapper
{
    public static partial GetConversationDetailsResponse MapToResponse(this Conversation conversation);
}
