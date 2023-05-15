using Utilities.Enums;

namespace Access.Contract.Invoices
{
    public record InvoiceAccessModel(
        Guid Id,
        string InvoiceNumber,
        int Iva10,
        int TotalIva,
        int SubTotal,
        int Total,
        string Timbrado,
        InvoiceStatus Status,
        Guid ClientId,
        DateTime DateCreated,
        string UserCreated,
        IEnumerable<InvoiceDetailAccessModel> InvoiceDetails
    );
}
