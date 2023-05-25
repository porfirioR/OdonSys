using Utilities.Enums;

namespace Contract.Pyment.Payments
{
    public record RegisterPaymentModel(
        string InvoiceId,
        string UserId,
        DateTime DateCreated,
        int Amount,
        InvoiceStatus Status
    );
}
