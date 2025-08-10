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

        response.Reverse();

        await SendOkAsync(response, ct);
    }
}
