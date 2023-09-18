using Contract.Administration.Clients;

namespace Contract.Administration.Reports
{
    public record ClientReportModel(
        ClientModel ClientModel,
        IEnumerable<ClientInvoiceReportModel> InvoiceModels
    );
}
