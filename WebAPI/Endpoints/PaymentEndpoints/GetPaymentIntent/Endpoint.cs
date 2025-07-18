using FastEndpoints;
using Stripe;

namespace WebAPI.Endpoints.PaymentEndpoints.GetPaymentIntent;

public class Endpoint : Endpoint<GetPaymentIntentRequest, GetPaymentIntentResponse>
{
    public override void Configure()
    {
        Get("/payment-intent/{PaymentIntentId}");
        AllowAnonymous();
        Description(x => x
            .WithName("Get Payment Intent")
            .Produces<GetPaymentIntentResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError));
        Group<PaymentGroup>();
    }

    public override async Task HandleAsync(GetPaymentIntentRequest req, CancellationToken ct)
    {
        var paymentIntentService = new PaymentIntentService();

        var paymentIntent = await paymentIntentService.GetAsync(req.PaymentIntentId, options: new PaymentIntentGetOptions()
        {
            ClientSecret = req.PaymentIntentId
        }, cancellationToken: ct);

        if (paymentIntent == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(paymentIntent.ToResponse(), ct);
    }
}

