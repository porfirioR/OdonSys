using Contract.Admin.Clients;
using Contract.Pyment.Invoices;
using Contract.Workspace.ClientProcedures;
using Contract.Workspace.Procedures;
using Host.Api.Models.ClientProcedures;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Json;

namespace AcceptanceTest.Host.Api.Payments
{
    internal partial class InvoiceControllerTest : TestBase
    {
        private readonly string _uri = "api/invoice";
        private readonly string _procedureUri = "api/procedure";
        private readonly string _clientUri = "api/clients";
        private readonly string _clientProcedureUri = "api/clientprocedure";

        [Test]
        [Order(1)]
        public async Task CreateInvoiceReturnOk()
        {
            var procedureRequest = CreateProcedureApiRequest;
            var procedureResponse = await _client.PostAsJsonAsync(_procedureUri, procedureRequest);
            var procedureActual = JsonConvert.DeserializeObject<ProcedureModel>(await procedureResponse.Content.ReadAsStringAsync());
            Assert.IsNotNull(procedureActual);

            var clientRequet = CreateClientApiRequest;
            var clientResponse = await _client.PostAsJsonAsync(_clientUri, clientRequet);
            var clientActual = JsonConvert.DeserializeObject<ClientModel>(await procedureResponse.Content.ReadAsStringAsync());
            Assert.IsNotNull(clientActual);

            var clientProcedureRequet = new CreateClientProcedureApiRequest
            {
                ClientId = clientActual.Id,
                ProcedureId = procedureActual.Id
            };
            var clientProcedureResponse = await _client.PostAsJsonAsync(_clientProcedureUri, clientProcedureRequet);
            var clientProcedureActual = JsonConvert.DeserializeObject<ClientProcedureModel>(await clientProcedureResponse.Content.ReadAsStringAsync());
            Assert.IsNotNull(clientProcedureActual);

            var request = CreateInvoiceApiRequest(clientActual.Id, clientProcedureActual.Id);
            var response = await _client.PostAsJsonAsync(_uri, request);

            var actual = JsonConvert.DeserializeObject<InvoiceModel>(await response.Content.ReadAsStringAsync());

            Assert.Multiple(() =>
            {
                Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
                Assert.That(request.InvoiceNumber, Is.EqualTo(actual.InvoiceNumber));
                Assert.That(request.Iva10, Is.EqualTo(actual.Iva10));
                Assert.That(request.TotalIva, Is.EqualTo(actual.TotalIva));
                Assert.That(request.SubTotal, Is.EqualTo(actual.SubTotal));
                Assert.That(request.Total, Is.EqualTo(actual.Total));
                Assert.That(request.Timbrado, Is.EqualTo(actual.Timbrado));
                Assert.That(request.Status, Is.EqualTo(actual.Status));
                Assert.That(request.ClientId, Is.EqualTo(actual.ClientId.ToString()));
                Assert.That(request.Timbrado, Is.EqualTo(actual.Timbrado));
                Assert.That(request.InvoiceDetails.Count(), Is.EqualTo(actual.InvoiceDetails.Count()));
            });
        }

        [Test]
        [Order(2)]
        public async Task UploadInvoiceFilesReturnOk()
        {
            var procedureRequest = CreateProcedureApiRequest;
            var procedureResponse = await _client.PostAsJsonAsync(_procedureUri, procedureRequest);
            var procedureContent = await procedureResponse.Content.ReadAsStringAsync();
            var procedureActual = JsonConvert.DeserializeObject<ProcedureModel>(procedureContent);
            Assert.IsNotNull(procedureActual);

            var clientRequet = CreateClientApiRequest;
            var clientResponse = await _client.PostAsJsonAsync(_clientUri, clientRequet);
            var content = await clientResponse.Content.ReadAsStringAsync();
            var clientActual = JsonConvert.DeserializeObject<ClientModel>(content);
            Assert.IsNotNull(clientActual);

            var clientProcedureRequet = new CreateClientProcedureApiRequest
            {
                ClientId = clientActual.Id,
                ProcedureId = procedureActual.Id
            };
            var clientProcedureResponse = await _client.PostAsJsonAsync(_clientProcedureUri, clientProcedureRequet);
            var clientProcedureContent = await clientProcedureResponse.Content.ReadAsStringAsync();
            var clientProcedureActual = JsonConvert.DeserializeObject<ClientProcedureModel>(clientProcedureContent);
            Assert.IsNotNull(clientProcedureActual);

            var invoiceRequest = CreateInvoiceApiRequest(clientActual.Id, clientProcedureActual.Id);
            var invoiceResponse = await _client.PostAsJsonAsync(_uri, invoiceRequest);
            var invoiceContent = await invoiceResponse.Content.ReadAsStringAsync();
            var invoiceActual = JsonConvert.DeserializeObject<InvoiceModel>(invoiceContent);
            Assert.IsNotNull(invoiceActual);

            var request = InvoiceFormData(invoiceActual.Id.ToString());
            var response = await _client.PostAsync($"{_uri}/upload-invoice-files", request);
            var actual = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(!string.IsNullOrEmpty(actual));
        }
    }
}
