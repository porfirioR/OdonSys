namespace Contract.Pyment.Payments
{
    public interface IPaymentManager
    {
        Task<PaymentModel> RegisterPaymentAsync(PaymentRequest accessRequest);
        Task<IEnumerable<PaymentModel>> GetPaymentsByBillIdAsync(string headerBillId);
    }
}
