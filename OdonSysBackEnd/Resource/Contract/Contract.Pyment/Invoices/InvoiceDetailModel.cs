namespace Contract.Pyment.Invoices
{
    public record InvoiceDetailModel(
        Guid Id,
        Guid InvoiceId,
        string Procedure,
        int ProcedurePrice,
        int FinalPrice
    );
}
