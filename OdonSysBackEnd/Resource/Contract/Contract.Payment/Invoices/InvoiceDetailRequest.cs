namespace Contract.Payment.Invoices;

public record InvoiceDetailRequest(
    Guid ClientProcedureId,
    int ProcedurePrice,
    int FinalPrice,
    IEnumerable<string> ToothIds
);
