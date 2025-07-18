using Riok.Mapperly.Abstractions;

namespace WebAPI.Endpoints.PaymentEndpoints.GetPaymentIntent;

[Mapper]
public static partial class Mapper
{
    public static partial GetPaymentIntentResponse ToResponse(this Stripe.PaymentIntent paymentIntent);
}
