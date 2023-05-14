using Access.Contract.Payments;
using Contract.Pyment.Payments;

namespace Manager.Payment
{
    internal class PaymentManager : IPaymentManager
    {
        private readonly IPaymentAccess _paymentAccess;

        public PaymentManager(IPaymentAccess paymentAccess)
        {
            _paymentAccess = paymentAccess;
        }

        public async Task<IEnumerable<PaymentModel>> GetPaymentsByInvoiceIdAsync(string invoiceId)
        {
            var accessModel = await _paymentAccess.GetPaymentsByInvoiceIdAsync(invoiceId);
            return accessModel.Select(x => new PaymentModel(x.InvoiceId, x.UserId, x.DateCreated, x.Amount));
        }

        public async Task<PaymentModel> RegisterPaymentAsync(PaymentRequest request)
        {
            var accessRequest = new PaymentAccessRequest(request.InvoiceId, request.UserId, request.Amount);
            var accessModel = await _paymentAccess.RegisterPayment(accessRequest);
            //var invoicePayments = (await _paymentAccess.GetPaymentsByInvoiceIdAsync(request.InvoiceId)).Select(x => x.Amount).Sum();

            return new PaymentModel(accessModel.InvoiceId, accessModel.UserId, accessModel.DateCreated, accessModel.Amount);
        }
    }
}
