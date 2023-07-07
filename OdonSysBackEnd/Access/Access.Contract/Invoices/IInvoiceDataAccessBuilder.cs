﻿using Access.Sql.Entities;

namespace Access.Contract.Invoices
{
    public interface IInvoiceDataAccessBuilder
    {
        Invoice MapInvoiceAccessRequestToInvoice(InvoiceAccessRequest request);
        InvoiceDetail MapInvoiceDetailAccessRequestToInvoiceDetail(InvoiceDetailAccessRequest request);
        InvoiceAccessModel GetModel(Invoice entity, IEnumerable<ClientProcedure> clientProcedureEntities);
    }
}
