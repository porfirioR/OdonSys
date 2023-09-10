namespace Contract.Payment.Invoices
{
    public record InvoiceDetailModel(
        Guid Id,
        Guid InvoiceId,
        string Procedure,
        int ProcedurePrice,
        int FinalPrice,
        DateTime DateCreated,
        string UserCreated,
        IEnumerable<string> ToothIds
    );
}
