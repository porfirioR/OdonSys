namespace Access.Contract.Invoices;

public record UpdateInvoiceDetailAccessRequest(
    Guid Id,
    int FinalPrice,
    IEnumerable<string> ToothIds
);
