namespace Access.Contract.Payments
{
    public interface IPaymentAccess
    {
        Task<IEnumerable<PaymentAccessModel>> GetPaymentsByInvoiceIdAsync(string invoiceId);
        Task<int> GetPaymentsAmountByInvoiceIdAsync(Guid invoiceId);
        Task<PaymentAccessModel> RegisterPayment(PaymentAccessRequest accessRequest);
    }
}
