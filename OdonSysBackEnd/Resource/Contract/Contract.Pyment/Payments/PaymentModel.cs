namespace Contract.Pyment.Payments
{
    public record PaymentModel(
        string HeaderBillId,
        string UserId,
        DateTime DateCreated,
        int Amount
    );
}
