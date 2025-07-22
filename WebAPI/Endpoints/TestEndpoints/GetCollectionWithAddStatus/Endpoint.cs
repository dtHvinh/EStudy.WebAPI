using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.TestEndpoints.GetCollectionWithAddStatus;

public class Endpoint(ApplicationDbContext context) : Endpoint<GetCollectionWithAddStatusRequest, List<GetCollectionWithAddStatusResponse>>
{
    private readonly ApplicationDbContext _context = context;
    public override void Configure()
    {
        Get("{TestId}/test-collections-with-add-status");
        AllowAnonymous();
        Description(x => x
            .WithSummary("Get test collections with add status")
            .WithDescription("This endpoint retrieves test collections with the status of whether a test can be added.")
            .Produces(StatusCodes.Status200OK));
        Group<TestGroup>();
    }
    public override async Task HandleAsync(GetCollectionWithAddStatusRequest request, CancellationToken ct)
    {
        var collections = await _context.TestCollections
            .Paginate(request.Page, request.PageSize)
            .Select(c => new GetCollectionWithAddStatusResponse
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                IsAdded = c.Tests.Any(t => t.Id == request.TestId)
            })
            .ToListAsync(ct);

        await SendOkAsync(collections, ct);
    }
}
