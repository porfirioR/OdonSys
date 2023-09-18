namespace Contract.Administration.Reports
{
    public record InvoiceDetailReportModel(
        Guid Id,
        string Procedure,
        int ProcedurePrice,
        int FinalPrice,
        DateTime DateCreated,
        IEnumerable<string> ToothIds
    );
}
