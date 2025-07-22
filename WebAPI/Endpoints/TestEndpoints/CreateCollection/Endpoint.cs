using FastEndpoints;
using WebAPI.Data;
using WebAPI.Models._testExam;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.TestEndpoints.CreateCollection;

public class Endpoint(ApplicationDbContext context) : Endpoint<CreateCollectionRequest, CreateCollectionResponse>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("/test-collections");
        AllowAnonymous();
        Description(x => x
            .WithSummary("Create a new test collection")
            .WithDescription("This endpoint allows users to create a new test collection.")
            .Produces(StatusCodes.Status200OK));
        Group<TestGroup>();
    }
    public override async Task HandleAsync(CreateCollectionRequest request, CancellationToken cancellationToken)
    {
        var collection = new TestCollection
        {
            Name = request.Name,
            Description = request.Description,
            AuthorId = int.Parse(this.RetrieveUserId())
        };

        _context.TestCollections.Add(collection);
        await _context.SaveChangesAsync(cancellationToken);

        await SendOkAsync(new CreateCollectionResponse()
        {
            Id = collection.Id,
            Name = collection.Name,
            Description = collection.Description
        }, cancellation: cancellationToken);
    }
}
