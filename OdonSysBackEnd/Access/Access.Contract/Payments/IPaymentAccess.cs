namespace Access.Contract.Payments
{
    public interface IPaymentAccess
    {
        Task<PaymentAccessModel> RegisterPayment(PaymentAccessRequest accessRequest);
        Task<IEnumerable<PaymentAccessModel>> GetPaymentsByInvoiceIdAsync(string invoiceId);
    }
}
