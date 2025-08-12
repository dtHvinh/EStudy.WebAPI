using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;
namespace WebAPI.Endpoints.AIEndpoints.GetConversationMessageList;

public class Endpoint(ApplicationDbContext context) : Endpoint<GetConversationMessageListRequest, List<GetConversationMessageListResponse>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("conversations/{ConversationId}/messages");
        AllowAnonymous();
        Description(x => x
            .WithName("Get Conversation Message List")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest));
        Group<AIGroup>();
    }
    public override async Task HandleAsync(GetConversationMessageListRequest req, CancellationToken ct)
    {
        var response = await _context.ConversationMessages
             .Where(x => x.ConversationId == req.ConversationId)
             .OrderByDescending(x => x.Timestamp)
             .Paginate(req.Page, req.PageSize)
             .Select(x => new GetConversationMessageListResponse
             {
                 Id = x.Id,
                 Message = x.Message,
                 Role = x.IsUserMessage ? "user" : "assistant",
                 Timestamp = x.Timestamp
             })
             .ToListAsync(ct);

        /**
         * The frontend use useSWRInfinite to fetch the conversation messages. Whenever the user scrolls to the top
         * it will fetch the previous page of messages. 
         * 
         * But we must not reserver the order here because:
         * 
         * There 2 things to consider here:
         * 
         * 1. When useSWRInfinite fetches the previous page, it will append the messages to the end of the list.
         * 2. If the list messages response has Id with order like this [12, 11 ,10, 9, 8] there are two posiblities here:
         *    + If we not reverse the response, the flattened list will be [12, 11 ,10, 9, 8] and when user scroll top it will fetch
         *      [7, 6, 5, 4, 3, 2] and the list will be [12, 11 ,10, 9, 8, 7, 6, 5, 4, 3, 2, 1] which is correct.
         *      
         *      Then the next thing will be reverse the list in the front end to show the messages in correct order.
         *      
         *    + If we reverse the response, the flattened list will be [8, 9, 10, 11, 12] and when user scroll top it will fetch
         *      [2,3, 4, 5, 6, 7] and the list will be [8, 9, 10, 11, 12, 2, 3, 4, 5, 6, 7] which is incorrect.
         *      
         */
        //response.Reverse();

        await SendOkAsync(response, ct);
    }
}
