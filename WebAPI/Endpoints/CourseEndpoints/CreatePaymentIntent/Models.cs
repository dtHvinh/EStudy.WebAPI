namespace WebAPI.Endpoints.CourseEndpoints.CreatePaymentIntent;

public sealed class CreatePaymentIntentRequest
{
    public int Id { get; set; }
}

public sealed class CreatePaymentIntentResponse
{
    public required string ClientSecret { get; set; } = default!;
}
