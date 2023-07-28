namespace Contract.Payment.Invoices
{
    public record InvoiceDetailRequest(
        Guid ClientProcedureId,
        int ProcedurePrice,
        int FinalPrice,
        string Color,
        string ToothId
    );
}
