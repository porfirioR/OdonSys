namespace Contract.Payment.Invoices
{
    public interface IInvoiceManager
    {
        Task<InvoiceModel> CreateInvoiceAsync(InvoiceRequest request);
        Task<bool> IsValidInvoiceIdAsync(string id);
        Task<IEnumerable<InvoiceModel>> GetInvoicesAsync();
        Task<InvoiceModel> GetInvoiceByIdAsync(string id);
        Task<IEnumerable<InvoiceModel>> GetMyInvoicesAsync(string username);
        Task<IEnumerable<InvoiceModel>> GetInvoicesSummaryByClientIdAsync(string clientId);
        Task<InvoiceModel> UpdateInvoiceStatusIdAsync(InvoiceStatusRequest request);
        Task<InvoiceModel> UpdateInvoiceAsync(UpdateInvoiceRequest accessRequest);

    }
}