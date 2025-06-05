namespace Access.Contract.Invoices;

public record UpdateInvoiceAccessRequest(
    Guid Id,
    int Iva10,
    int TotalIva,
    int SubTotal,
    int Total,
    IEnumerable<UpdateInvoiceDetailAccessRequest> InvoiceDetails
);
