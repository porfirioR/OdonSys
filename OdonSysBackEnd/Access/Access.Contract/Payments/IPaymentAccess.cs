namespace Access.Contract.Payments;

public interface IPaymentAccess
{
    Task<IEnumerable<PaymentAccessModel>> GetPaymentsByInvoiceIdAsync(string invoiceId);
    Task<IEnumerable<PaymentAmountAccessModel>> GetPaymentsAmountByInvoiceIdAsync(IEnumerable<Guid> invoiceIds);
    Task<PaymentAccessModel> RegisterPayment(PaymentAccessRequest accessRequest);
}
