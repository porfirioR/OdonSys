using Access.Contract.Invoices;
using Access.Contract.Payments;
using Contract.Pyment.Payments;
using Utilities.Enums;

namespace Manager.Payment
{
    internal class PaymentManager : IPaymentManager
    {
        private readonly IPaymentAccess _paymentAccess;
        private readonly IInvoiceAccess _invoiceAccess;

        public PaymentManager(IPaymentAccess paymentAccess, IInvoiceAccess invoiceAccess)
        {
            _paymentAccess = paymentAccess;
            _invoiceAccess = invoiceAccess;
        }

        public async Task<IEnumerable<PaymentModel>> GetPaymentsByInvoiceIdAsync(string invoiceId)
        {
            var accessModel = await _paymentAccess.GetPaymentsByInvoiceIdAsync(invoiceId);
            return accessModel.Select(x => new PaymentModel(x.InvoiceId, x.UserId, x.DateCreated, x.Amount));
        }

        public async Task<RegisterPaymentModel> RegisterPaymentAsync(PaymentRequest request)
        {
            var accessRequest = new PaymentAccessRequest(request.InvoiceId, request.UserId, request.Amount);
            var accessModel = await _paymentAccess.RegisterPayment(accessRequest);
            var invoice = await _invoiceAccess.GetInvoiceByIdAsync(request.InvoiceId);
            var invoicePayments = (await _paymentAccess.GetPaymentsByInvoiceIdAsync(request.InvoiceId)).Select(x => x.Amount).Sum();
            var status = invoice.Status;
            if (invoice.Total == invoicePayments)
            {
                status = InvoiceStatus.Completado;
            }
            else if (invoice.Total > invoicePayments)
            {
                status = InvoiceStatus.Pendiente;
            }
            if (invoice.Status != status)
            {
                await _invoiceAccess.UpdateInvoiceStatusIdAsync(new InvoiceStatusAccessRequest(request.InvoiceId, status));
            }
            return new RegisterPaymentModel(accessModel.InvoiceId, accessModel.UserId, accessModel.DateCreated, accessModel.Amount, status);
        }
    }
}
