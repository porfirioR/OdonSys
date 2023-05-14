using Utilities.Enums;

namespace Access.Contract.Invoices
{
    public record InvoiceAccessRequest(
        string InvoiceNumber,
        int Iva10,
        int TotalIva,
        int SubTotal,
        int Total,
        string Timbrado,
        InvoiceStatus Status,
        Guid ClientId,
        IEnumerable<InvoiceDetailAccessRequest> InvoiceDetails
    );
}
