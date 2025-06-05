using Utilities.Enums;

namespace Access.Contract.Invoices;

public record InvoiceStatusAccessRequest(
    string InvoiceId,
    InvoiceStatus Status
);
