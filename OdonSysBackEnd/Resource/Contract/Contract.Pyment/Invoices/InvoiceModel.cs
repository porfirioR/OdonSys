using Utilities.Enums;

namespace Contract.Pyment.Invoices
{
    public record InvoiceModel(
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
        IEnumerable<InvoiceDetailModel> InvoiceDetails
    );
}
