namespace Access.Contract.Invoices
{
    public record InvoiceDetailAccessModel(
        Guid Id,
        Guid InvoiceId,
        string Procedure,
        int ProcedurePrice,
        int FinalPrice,
        DateTime DateCreated,
        string UserCreated
    );
}
