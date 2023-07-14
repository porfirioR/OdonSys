using Host.Api.Contract.Clients;
using Host.Api.Contract.Invoices;
using Host.Api.Contract.Procedures;
using Utilities.Enums;

namespace AcceptanceTest.Host.Api.Payments
{
    internal partial class InvoiceControllerTest
    {

        CreateProcedureApiRequest CreateProcedureApiRequest => new()
        {
            Name = Guid.NewGuid().ToString()[..30],
            Description = Guid.NewGuid().ToString()[..30],
            Price = 10,
            //ProcedureTeeth = TeethIds
        };

        static CreateClientApiRequest CreateClientApiRequest => new()
        {
            Name = Guid.NewGuid().ToString()[..5],
            Surname = Guid.NewGuid().ToString()[..5],
            SecondSurname = Guid.NewGuid().ToString()[..5],
            Document = Guid.NewGuid().ToString()[..7],
            Country = Country.Argentina,
            Phone = Guid.NewGuid().ToString()[..8],
            Email = $"{Guid.NewGuid().ToString()[..6]}@{Guid.NewGuid().ToString()[..6]}.com"
        };

        static CreateInvoiceApiRequest CreateInvoiceApiRequest(string clientId, string clientProcedureId) => new()
        {
            InvoiceNumber = "abc",
            Iva10 = 1,
            TotalIva = 1,
            SubTotal = 10,
            Total = 10,
            Timbrado = "111",
            Status = InvoiceStatus.Nuevo,
            InvoiceDetails = new List<CreateInvoiceDetailApiRequest>()
            {
                new CreateInvoiceDetailApiRequest
                {
                    ClientProcedureId = clientProcedureId,
                    FinalPrice = 10,
                    ProcedurePrice = 10
                }
            },
            ClientId = clientId
        };

        static MultipartFormDataContent InvoiceFormData(string invoiceId) => new()
        {
            { new StringContent(invoiceId), "Id" },
            { new ByteArrayContent(Properties.Resources.ImageTest), "Files", $"{Guid.NewGuid().ToString()[..10]}.png" },
        };

        static MultipartFormDataContent InvoiceFormDataWithPdf(string invoiceId) => new()
        {
            { new StringContent(invoiceId), "Id" },
            { new ByteArrayContent(Properties.Resources.ImageTest), "Files", $"{Guid.NewGuid().ToString()[..10]}.png" },
            { new ByteArrayContent(Properties.Resources.TestPdf), "Files", $"{Guid.NewGuid().ToString()[..10]}.pdf" },
        };
    }
}
