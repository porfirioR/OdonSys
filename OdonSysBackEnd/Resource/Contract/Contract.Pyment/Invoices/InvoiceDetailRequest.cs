namespace Contract.Pyment.Invoices
{
    public record InvoiceDetailRequest(
        Guid ClientProcedureId,
        int ProducePrice,
        int FinalPrice
    );
}
