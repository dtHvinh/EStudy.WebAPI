using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Endpoints.TestEndpoints.GetCollectionContent;

public class Endpoint(ApplicationDbContext context) : Endpoint<GetCollectionContentRequest, GetCollectionContentResponse>
{
    private readonly ApplicationDbContext _context = context;
    public override void Configure()
    {
        Get("/test-collections/{CollectionId}");
        AllowAnonymous();
        Description(x => x
            .WithSummary("Get test collection content")
            .WithDescription("This endpoint retrieves the content of a specific test collection.")
            .Produces(StatusCodes.Status200OK));
        Group<TestGroup>();
    }
    public override async Task HandleAsync(GetCollectionContentRequest request, CancellationToken ct)
    {
        var collection = await _context.TestCollections
            .Where(c => c.Id == request.CollectionId)
            .Select(c => new GetCollectionContentResponse
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Tests = c.Tests.Select(t => new GetCollectionContentTestResponse
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Duration = t.Duration,
                    AuthorName = t.Creator.Name,
                    SectionCount = t.Sections.Count,
                    AttemptCount = t.Attempts.Count,
                    CommentCount = t.Comments.Count,
                    QuestionCount = t.Sections.SelectMany(e => e.Questions).DefaultIfEmpty().Count()
                }).ToList()
            })
            .FirstOrDefaultAsync(ct);
        if (collection == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(collection, ct);
    }
}
