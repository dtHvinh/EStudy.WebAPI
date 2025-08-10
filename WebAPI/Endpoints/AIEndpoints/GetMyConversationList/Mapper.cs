using Riok.Mapperly.Abstractions;
using WebAPI.Models._ai;

namespace WebAPI.Endpoints.AIEndpoints.GetMyConversationList;

[Mapper]
public static partial class Mapper
{
    public static IQueryable<GetMyConversationListResponse> ProjectToResponse(this IQueryable<Conversation> conversations)
    {
        return Queryable.Select(
                   conversations,
                   x => new GetMyConversationListResponse()
                   {
                       Id = x.Id,
                       Name = x.Name,
                       Description = x.Description,
                       Context = x.Context,
                       CreationDate = x.CreationDate,
                       LastActive = x.LastActive,
                       MessageCount = x.Messages.Count
                   }
               );
    }
}
