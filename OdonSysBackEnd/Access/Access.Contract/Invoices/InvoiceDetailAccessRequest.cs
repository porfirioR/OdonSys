﻿namespace Access.Contract.Invoices
{
    public record InvoiceDetailAccessRequest(
        Guid ClientProcedureId,
        int ProducePrice,
        int FinalPrice
    );
}