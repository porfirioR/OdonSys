namespace Access.Contract.Payments
{
    public record PaymentAccessModel(
        string HeaderBillId,
        string UserId,
        int Amount
    );
}
