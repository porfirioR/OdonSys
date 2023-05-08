namespace Access.Contract.Payments
{
    public interface IPaymentAccess
    {
        Task<PaymentAccessModel> RegisterPayment(PaymentAccessRequest accessRequest);

    }
}
