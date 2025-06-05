namespace Access.Contract.Invoices;

public interface IInvoiceAccess
{
    Task<InvoiceAccessModel> CreateInvoiceAsync(InvoiceAccessRequest accessRequest);
    Task<IEnumerable<InvoiceAccessModel>> GetInvoicesAsync();
    Task<IEnumerable<InvoiceAccessModel>> GetInvoicesByClientIdAsync(string clientId);
    Task<InvoiceAccessModel> GetInvoiceByIdAsync(string id);
    Task<bool> IsValidInvoiceIdAsync(string id);
    Task<InvoiceAccessModel> UpdateInvoiceStatusIdAsync(InvoiceStatusAccessRequest accessRequest);
    Task<InvoiceAccessModel> UpdateInvoiceAsync(UpdateInvoiceAccessRequest accessRequest);
}
