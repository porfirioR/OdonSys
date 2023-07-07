using Access.Contract.Payments;
using Access.Sql.Entities;

namespace Access.Data.Mapper
{
    internal class PaymentDataAccessBuilder : IPaymentDataAccessBuilder
    {
        public Payment MapPaymentAccessRequestToPayment(PaymentAccessRequest request)
        {
            var payment = new Payment()
            {
                UserId = new Guid(request.UserId),
                InvoiceId = new Guid(request.InvoiceId),
                Amount = request.Amount,
                Active = true
            };
            return payment;
        }

        public PaymentAccessModel MapPaymentToPaymentAccessModel(Payment payment) => new (
            payment.InvoiceId.ToString(),
            payment.UserId.ToString(),
            payment.DateCreated,
            payment.Amount
        );
    }
}
