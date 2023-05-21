using Utilities.Enums;

namespace Contract.Pyment.Invoices
{
    public record InvoiceStatusRequest(
        string InvoiceId,
        InvoiceStatus Status
    );
}
