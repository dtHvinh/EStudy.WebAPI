using FastEndpoints;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.TestEndpoints.CreateTest;

public class Endpoints(ApplicationDbContext context) : Endpoint<CreateTestRequest, CreateTestResponse>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("");
        Group<TestGroup>();
    }

    public override async Task HandleAsync(CreateTestRequest req, CancellationToken ct)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(ct);

        try
        {
            var test = req.ToTest(this.RetrieveUserId());
            _context.TestExams.Add(test);
            await _context.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);

            await SendOkAsync(new CreateTestResponse() { Id = test.Id }, ct);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(ct);
            ThrowError("Fail to create test");
        }
    }
}