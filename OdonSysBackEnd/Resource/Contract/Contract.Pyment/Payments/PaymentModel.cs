namespace Contract.Pyment.Payments
{
    public record PaymentModel(
        string InvoiceId,
        string UserId,
        DateTime DateCreated,
        int Amount
    );
}
