using Utilities.Enums;

namespace Contract.Payment.Invoices
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
    )
    {
        public int PaymentAmount { get; set; }
    }
}
