namespace Contract.Pyment.Invoices
{
    public record InvoiceDetailRequest(
        Guid ClientProcedureId,
        int ProcedurePrice,
        int FinalPrice
    );
}
