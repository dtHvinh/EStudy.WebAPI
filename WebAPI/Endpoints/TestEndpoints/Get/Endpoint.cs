using FastEndpoints;

namespace WebAPI.Endpoints.TestEndpoints.Get;

internal sealed class Endpoint : EndpointWithoutRequest<TestGetResponse>
{
    public override void Configure()
    {
        Get("");
        AllowAnonymous();
        Group<TestGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendOkAsync(new($"You did it on {DateTime.UtcNow:f}"), ct);
    }
}