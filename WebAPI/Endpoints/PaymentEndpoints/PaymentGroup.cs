using FastEndpoints;

namespace WebAPI.Endpoints.PaymentEndpoints;

public class PaymentGroup : Group
{
    public PaymentGroup()
    {
        Configure("payments", ep =>
        {
        });
    }
}
