using Utilities.Enums;

namespace Contract.Pyment.Invoices
{
    public record InvoiceRequest(
        string InvoiceNumber,
        int Iva10,
        int TotalIva,
        int SubTotal,
        int Total,
        string Timbrado,
        InvoiceStatus Status,
        Guid ClientId,
        IEnumerable<InvoiceDetailRequest> InvoiceDetails
    );
}
