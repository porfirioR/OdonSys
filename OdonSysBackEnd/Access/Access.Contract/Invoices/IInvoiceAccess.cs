namespace Access.Contract.Invoices
{
    public interface IInvoiceAccess
    {
        Task<InvoiceAccessModel> CreateInvoiceAsync(InvoiceAccessRequest accessRequest);
        Task<IEnumerable<InvoiceAccessModel>> GetInvoicesAsync();
        Task<InvoiceAccessModel> GetInvoiceByIdAsync(string id);
        Task<bool> IsValidInvoiceIdAsync(string id);
        Task<InvoiceAccessModel> UpdateInvoiceStatusIdAsync(InvoiceStatusAccessRequest accessRequest);
    }
}
