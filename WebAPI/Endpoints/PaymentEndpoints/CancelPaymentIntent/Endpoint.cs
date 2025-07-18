using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Stripe;
using WebAPI.Data;
using WebAPI.Endpoints.PaymentEndpoints.GetPaymentIntent;

namespace WebAPI.Endpoints.PaymentEndpoints.CancelPaymentIntent;

public class Endpoint(ApplicationDbContext context) : Endpoint<CancelPaymentIntentRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Delete("/payment-intent/{PaymentIntentId}");
        AllowAnonymous();
        Description(x => x
            .WithName("Cancel Payment Intent")
            .Produces<GetPaymentIntentResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError));
    }

    public override async Task HandleAsync(CancelPaymentIntentRequest req, CancellationToken ct)
    {
        var transaction = await _context.Transactions.FirstOrDefaultAsync(e => e.PaymentIntentId == req.PaymentIntentId, ct);

        if (transaction is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var paymentIntentService = new PaymentIntentService();
        await paymentIntentService.CancelAsync(req.PaymentIntentId, cancellationToken: ct);

        _context.Remove(transaction);
        await _context.SaveChangesAsync(ct);
    }
}
