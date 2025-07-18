namespace WebAPI.Constants;

public static class PaymentIntentStatus
{
    public const string Canceled = "canceled";
    public const string Processing = "processing";
    public const string RequiresAction = "requires_action";
    public const string RequiresCapture = "requires_capture";
    public const string RequiresConfirmation = "requires_confirmation";
    public const string RequiresPaymentMethod = "requires_payment_method";
    public const string Succeeded = "succeeded";
}