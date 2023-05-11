namespace Contract.Pyment.Payments
{
    public record PaymentRequest(
        string HeaderBillId,
        string UserId,
        int Amount
    );
}
