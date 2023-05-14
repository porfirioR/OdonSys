namespace Access.Contract.Invoices
{
    public interface IInvoiceAccess
    {
        Task<InvoiceAccessModel> CreateInvoiceAsync(InvoiceAccessRequest accessRequest);
        Task<IEnumerable<InvoiceAccessModel>> GetInvoicesAsync();
        Task<bool> IsValidInvoiceIdAsync(string id);
    }
}
