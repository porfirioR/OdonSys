using Utilities.Enums;

namespace Contract.Payment.Invoices
{
    public record InvoiceStatusRequest(
        string InvoiceId,
        InvoiceStatus Status
    );
}
