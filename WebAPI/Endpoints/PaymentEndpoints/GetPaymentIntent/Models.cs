#nullable disable

namespace WebAPI.Endpoints.PaymentEndpoints.GetPaymentIntent;

public sealed class GetPaymentIntentRequest
{
    public required string PaymentIntentId { get; set; } = default!;
}

public sealed class GetPaymentIntentResponse
{
    public long Amount { get; set; }
    public string Currency { get; set; } = default!;
    public Dictionary<string, string> Metadata { get; set; }
}