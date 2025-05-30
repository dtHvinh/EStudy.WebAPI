using FastEndpoints;
using WebAPI.Endpoints.Groups;

namespace WebAPI.Endpoints.TestEndpoints;

public class Get : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("");
        AllowAnonymous();
        Description(cf =>
        {
            cf.WithSummary("Test api");
        });
        Group<TestGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendOkAsync(new
        {
            Message = $"You did it on {DateTime.UtcNow:f}"
        }, ct);
    }
}
