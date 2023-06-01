namespace Contract.Pyment.Invoices
{
    public record InvoiceDetailModel(
        Guid Id,
        Guid InvoiceId,
        Guid ProcedureId,
        string Procedure,
        int ProcedurePrice,
        int FinalPrice
    );
}
