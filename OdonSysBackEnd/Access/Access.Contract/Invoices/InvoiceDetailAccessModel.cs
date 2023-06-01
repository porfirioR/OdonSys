namespace Access.Contract.Invoices
{
    public record InvoiceDetailAccessModel(
        Guid Id,
        Guid InvoiceId,
        Guid ProcedureId,
        string Procedure,
        int ProcedurePrice,
        int FinalPrice
    );
}
