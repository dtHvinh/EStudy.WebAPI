using Microsoft.EntityFrameworkCore;
using Stripe;
using WebAPI.Data;
using WebAPI.Models._payment;
using WebAPI.Models.Contract;
using WebAPI.Utilities.Attributes;
using WebAPI.Utilities.Converters.Implementations;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Services;

public sealed class CreatePaymentIntentOptions
{
    public Dictionary<string, string> Metadata { get; set; } = [];
}

[Service(ServiceLifetime.Scoped)]
public class PaymentService(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;
    private IQueryable<Transaction> Table => _context.Transactions;

    public async Task<string> CreateOrGetClientSecret(
        IPurchasable<int> purchasable,
        string consumerId,
        CreatePaymentIntentOptions? createOptions = null, CancellationToken ct = default)
    {
        var transaction = await Table
            .OrderByDescending(e => e.CreationDate)
            .FirstOrDefaultAsync(e => e.ProductType == purchasable.GetType().Name
                                  && e.ProductId.Equals(purchasable.Id)
                                  && e.CustomerId == int.Parse(consumerId), ct);

        var paymentIntentService = new PaymentIntentService();
        PaymentIntent paymentIntent;

        if (transaction is not null)
        {
            paymentIntent = await paymentIntentService.GetAsync(transaction.PaymentIntentId, cancellationToken: ct);

            if (paymentIntent.Status == "canceled")
            {

            }
            else
            {
                return paymentIntent.ClientSecret;
            }
        }

        var options = new PaymentIntentCreateOptions
        {
            Amount = purchasable.Price.CurrencyConvert(new DecimalUsdToCentUsdConverter()),
            Currency = "usd",
            AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
            {
                Enabled = true,
            },
        };

        if (createOptions is not null)
        {
            options.Metadata = createOptions.Metadata;
        }

        paymentIntent = paymentIntentService.Create(options);
        _context.Transactions.Add(new Transaction
        {
            CreationDate = DateTimeOffset.UtcNow,
            CustomerId = int.Parse(consumerId),
            ProductId = purchasable.Id,
            ProductType = purchasable.GetType().Name,
            PaymentIntentId = paymentIntent.Id,
            Status = paymentIntent.Status,
        });

        await _context.SaveChangesAsync(ct);

        return paymentIntent.ClientSecret;
    }
}
