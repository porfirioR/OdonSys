using Access.Contract.Invoices;
using Access.Contract.Payments;
using Contract.Payment.Invoices;

namespace Manager.Payment
{
    internal sealed class InvoiceManager : IInvoiceManager
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
                    x.ProcedurePrice,
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

        public async Task<IEnumerable<InvoiceModel>> GetMyInvoicesAsync(string userName)
        {
            var invoiceAccessModelList = await _invoiceAccess.GetInvoicesAsync();
            var myInvoiceAccessList = invoiceAccessModelList.Where(x => x.UserCreated == userName);
            return await PrepareInvocesWithPayments(myInvoiceAccessList);
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

        public async Task<IEnumerable<InvoiceModel>> GetInvoicesSummaryByClientIdAsync(string clientId)
        {
            var accessModelList = await _invoiceAccess.GetInvoicesAsync();
            accessModelList = accessModelList.Where(x => x.ClientId == new Guid(clientId));
            return accessModelList.Select(GetModel);
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
                accessModel.ClientFullName,
                accessModel.DateCreated,
                accessModel.UserCreated,
                accessModel.InvoiceDetails.Select(x => new InvoiceDetailModel(
                    x.Id,
                    x.InvoiceId,
                    x.Procedure,
                    x.ProcedurePrice,
                    x.FinalPrice,
                    x.DateCreated,
                    x.UserCreated
                ))
            );
        }
    }
}