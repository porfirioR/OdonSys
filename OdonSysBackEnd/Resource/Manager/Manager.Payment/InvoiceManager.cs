using Access.Contract.Invoices;
using Access.Contract.Payments;
using Contract.Pyment.Invoices;

namespace Manager.Payment
{
    internal class InvoiceManager : IInvoiceManager
    {
        private readonly IInvoiceAccess _invoiceAccess;
        private readonly IPaymentAccess _paymentAccess;

        public InvoiceManager(IInvoiceAccess invoiceAccess, IPaymentAccess paymentAccess)
        {
            _invoiceAccess = invoiceAccess;
            _paymentAccess = paymentAccess;
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
            return await PrepareInvocesWithPayments(invoices);
        }

        public async Task<InvoiceModel> GetInvoiceByIdAsync(string id)
        {
            var invoiceAccessModel = await _invoiceAccess.GetInvoiceByIdAsync(id);
            var invoiceModel = (await PrepareInvocesWithPayments(new List<InvoiceAccessModel> { invoiceAccessModel })).First();
            return invoiceModel;
        }

        public async Task<IEnumerable<InvoiceModel>> GetMyInvoicesAsync(string username)
        {
            var invoiceAccessModelList = await _invoiceAccess.GetInvoicesAsync();
            var myInvoiceAccessList = invoiceAccessModelList.Where(x => x.UserCreated == username);
            return await PrepareInvocesWithPayments(invoiceAccessModelList);
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

        private async Task<IEnumerable<InvoiceModel>> PrepareInvocesWithPayments(IEnumerable<InvoiceAccessModel> invoiceIdsList)
        {
            var invoiceIds = invoiceIdsList.Select(x => x.Id);
            var paymentsAmounts = await _paymentAccess.GetPaymentsAmountByInvoiceIdAsync(invoiceIds);
            var invoiceModelList = invoiceIdsList.Select(invoice =>
            {
                var invoiceModel = GetModel(invoice);
                invoiceModel.PaymentAmount = paymentsAmounts.FirstOrDefault(x => x.InvoiceId == invoice.Id)?.PaymentAmount ?? 0;
                return invoiceModel;
            });
            return invoiceModelList;
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
                    x.ProcedureId,
                    x.Procedure,
                    x.ProcedurePrice,
                    x.FinalPrice
                ))
            );
        }
    }
}