namespace Contract.Payment.Invoices
{
    public record UpdateInvoiceRequest(
        Guid Id,
        int Iva10,
        int TotalIva,
        int SubTotal,
        int Total,
        IEnumerable<UpdateInvoiceDetailRequest> InvoiceDetails
    );
}
