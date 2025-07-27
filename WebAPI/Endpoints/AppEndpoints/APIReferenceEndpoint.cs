using FastEndpoints;
using WebAPI.Constants.EndpointConstant;

namespace WebAPI.Endpoints.AppEndpoints;

public class APIReferenceEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("");
        AllowAnonymous();
        Description(x => x
            .WithTags("API Reference")
            .WithSummary("API Reference Documentation")
            .WithDescription("This endpoint provides access to the API reference documentation."));
        Tags(ETags.APIReference);
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendRedirectAsync("/scalar/v1", true);
    }
}
