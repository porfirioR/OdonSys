namespace Contract.Pyment.Payments
{
    public record PaymentRequest(
        string InvoiceId,
        string UserId,
        int Amount
    );
}
