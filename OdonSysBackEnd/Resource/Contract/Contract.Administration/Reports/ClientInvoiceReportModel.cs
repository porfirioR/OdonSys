namespace Contract.Administration.Reports
{
    public record ClientInvoiceReportModel(
        Guid Id,
        int Total,
        DateTime DateCreated,
        IEnumerable<InvoiceDetailReportModel> InvoiceDetails
    );
}
