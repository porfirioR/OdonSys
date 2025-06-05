namespace Access.Contract.Payments;

public record PaymentAccessRequest(
    string InvoiceId,
    string UserId,
    int Amount
);
