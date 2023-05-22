using Access.Contract.Invoices;
using Contract.Pyment.Invoices;

namespace Manager.Payment
{
    internal class InvoiceManager : IInvoiceManager
    {
        private readonly IInvoiceAccess _invoiceAccess;

        public InvoiceManager(IInvoiceAccess invoiceAccess)
        {
            _invoiceAccess = invoiceAccess;
        }

        public async Task<InvoiceModel> CreateInvoiceAsync(InvoiceRequest request)
        {
            var accessRequest = new InvoiceAccessRequest(
                request.InvoiceNumber,
                request.Iva10,
                request.TotalIva,
                request.SubTotal,
                request.Total,
                request.Timbrado,
                request.Status,
                request.ClientId,
                request.InvoiceDetails.Select(x => new InvoiceDetailAccessRequest(
                    x.ClientProcedureId,
                    x.ProducePrice,
                    x.FinalPrice))
            );
            var accessModel = await _invoiceAccess.CreateInvoiceAsync(accessRequest);
            var model = GetModel(accessModel);
            return model;
        }

        public async Task<IEnumerable<InvoiceModel>> GetInvoicesAsync()
        {
            var invoices = await _invoiceAccess.GetInvoicesAsync();
            return invoices.Select(x => GetModel(x));
        }

        public async Task<bool> IsValidInvoiceIdAsync(string id)
        {
            return await _invoiceAccess.IsValidInvoiceIdAsync(id);
        }

        public async Task<InvoiceModel> UpdateInvoiceStatusIdAsync(InvoiceStatusRequest request)
        {
            var accessModel = await _invoiceAccess.UpdateInvoiceStatusIdAsync(new InvoiceStatusAccessRequest(request.InvoiceId, request.Status));
            return GetModel(accessModel);
        }

        private static InvoiceModel GetModel(InvoiceAccessModel accessModel)
        {
            return new InvoiceModel(
                accessModel.Id,
                accessModel.InvoiceNumber,
                accessModel.Iva10,
                accessModel.TotalIva,
                accessModel.SubTotal,
                accessModel.Total,
                accessModel.Timbrado,
                accessModel.Status,
                accessModel.ClientId,
                accessModel.DateCreated,
                accessModel.UserCreated,
                accessModel.InvoiceDetails.Select(x => new InvoiceDetailModel(
                    x.Id,
                    x.InvoiceId,
                    x.Procedure,
                    x.ProcedurePrice,
                    x.FinalPrice))
            );
        }
    }
}