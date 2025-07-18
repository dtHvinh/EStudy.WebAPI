using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Stripe;
using WebAPI.Constants;
using WebAPI.Data;

namespace WebAPI.Endpoints.PaymentEndpoints.Webhook;

public class Endpoint(Serilog.ILogger logger, ApplicationDbContext context) : EndpointWithoutRequest
{
    private readonly Serilog.ILogger _logger = logger;
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("webhook");
        AllowAnonymous();
        Tags("hook");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync(ct);
        try
        {
            var stripeEvent = EventUtility.ConstructEvent(json,
                HttpContext.Request.Headers["Stripe-Signature"], Config["Stripe:WebhookSecret"]);

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    _logger.Information("Payment succeeded: {PaymentIntentId}", stripeEvent.Data);
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    await HandlePaymentIntentSuccess(paymentIntent!, ct);
                    break;
                default:
                    _logger.Information("Unhandled event type: {EventType}", stripeEvent.Type);
                    break;
            }

            await SendOkAsync(ct);
        }
        catch (StripeException)
        {
            ThrowError("Stripe error occurred");
        }
    }

    private async Task HandlePaymentIntentSuccess(PaymentIntent paymentIntent, CancellationToken ct = default)
    {
        var transaction = await _context.Transactions.FirstOrDefaultAsync(e => e.PaymentIntentId == paymentIntent.Id, ct);

        if (transaction is null)
            return;

        transaction.Status = PaymentIntentStatus.Succeeded;

        var type = transaction.ProductType;

        if (type == "Course")
        {
            await HandleCoursePaymentSuccess(transaction.ProductId, transaction.CustomerId, ct);
        }
    }

    private async Task HandleCoursePaymentSuccess(int courseId, int userId, CancellationToken ct = default)
    {
        _context.CourseEnrollments.Add(new()
        {
            CourseId = courseId,
            UserId = userId,
            EnrollmentDate = DateTimeOffset.UtcNow
        });

        await _context.SaveChangesAsync(ct);
    }
}
