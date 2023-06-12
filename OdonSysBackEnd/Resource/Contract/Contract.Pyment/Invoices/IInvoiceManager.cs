using Contract.Workspace.Files;

namespace Contract.Pyment.Invoices
{
    public interface IInvoiceManager
    {
        Task<InvoiceModel> CreateInvoiceAsync(InvoiceRequest request);
        Task<bool> IsValidInvoiceIdAsync(string id);
        Task<IEnumerable<InvoiceModel>> GetInvoicesAsync();
        Task<InvoiceModel> GetInvoiceByIdAsync(string id);
        Task<IEnumerable<InvoiceModel>> GetMyInvoicesAsync(string username);
        Task<InvoiceModel> UpdateInvoiceStatusIdAsync(InvoiceStatusRequest request);
    }
}