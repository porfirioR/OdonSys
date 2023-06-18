namespace Contract.Payment.Payments
{
    public record PaymentRequest(
        string InvoiceId,
        string UserId,
        int Amount
    );
}
