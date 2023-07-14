using Access.Sql.Entities;

namespace Access.Contract.Payments
{
    public interface IPaymentDataAccessBuilder
    {
        Payment MapPaymentAccessRequestToPayment(PaymentAccessRequest request);
        PaymentAccessModel MapPaymentToPaymentAccessModel(Payment payment);
    }
}
