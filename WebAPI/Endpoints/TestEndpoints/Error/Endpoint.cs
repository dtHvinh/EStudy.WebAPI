using FastEndpoints;

namespace WebAPI.Endpoints.TestEndpoints.Error;

sealed class Endpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("error");
        Group<TestGroup>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken c)
    {
        await Task.CompletedTask;

        AddError("Error 1");
        AddError("Error 2");

        ThrowError("This is error message");
    }
}
