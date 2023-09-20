namespace Contract.Payment.Invoices
{
    public record UpdateInvoiceDetailRequest(
        Guid Id,
        int FinalPrice,
        IEnumerable<string> ToothIds
    );
}
