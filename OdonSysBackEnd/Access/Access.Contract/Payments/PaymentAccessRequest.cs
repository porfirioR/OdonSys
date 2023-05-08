namespace Access.Contract.Payments
{
    public record PaymentAccessRequest(
        string HeaderBillId,
        string UserId,
        int Amount
    );
}
