namespace Access.Contract.Payments;

public record PaymentAccessModel(
    string InvoiceId,
    string UserId,
    DateTime DateCreated,
    int Amount
);
