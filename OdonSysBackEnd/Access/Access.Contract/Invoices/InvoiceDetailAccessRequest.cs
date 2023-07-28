namespace Access.Contract.Invoices
{
    public record InvoiceDetailAccessRequest(
        Guid ClientProcedureId,
        int ProcedurePrice,
        int FinalPrice,
        string Color,
        string ToothId
    );
}
