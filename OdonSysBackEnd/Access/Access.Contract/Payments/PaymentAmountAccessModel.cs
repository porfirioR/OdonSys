namespace Access.Contract.Payments
{
    public record PaymentAmountAccessModel(
        Guid InvoiceId,
        int PaymentAmount
    );
}
