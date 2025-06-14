﻿using Contract.Administration.Clients;
using Contract.Payment.Invoices;
using Contract.Workspace.ClientProcedures;
using Contract.Workspace.Files;
using Contract.Workspace.Procedures;
using Host.Api.Contract.ClientProcedures;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Json;

namespace AcceptanceTest.Host.Api.Payments;

internal partial class InvoiceControllerTest : TestBase
{
    private readonly string _uri = "api/invoice";
    private readonly string _procedureUri = "api/procedure";
    private readonly string _clientUri = "api/clients";
    private readonly string _clientProcedureUri = "api/clientprocedure";

    [Test]
    [Order(1)]
    public async Task ShouldCreateInvoiceReturnOk()
    {
        var procedureRequest = CreateProcedureApiRequest;
        var procedureResponse = await _client.PostAsJsonAsync(_procedureUri, procedureRequest);
        var procedureActual = JsonConvert.DeserializeObject<ProcedureModel>(await procedureResponse.Content.ReadAsStringAsync());
        Assert.That(procedureActual, Is.Not.Null);

        var clientRequet = CreateClientApiRequest;
        var clientResponse = await _client.PostAsJsonAsync(_clientUri, clientRequet);
        var clientActual = JsonConvert.DeserializeObject<ClientModel>(await procedureResponse.Content.ReadAsStringAsync());
        Assert.That(clientActual, Is.Not.Null);

        var clientProcedureRequet = new CreateClientProcedureApiRequest
        {
            ClientId = clientActual.Id,
            ProcedureId = procedureActual.Id
        };
        var clientProcedureResponse = await _client.PostAsJsonAsync(_clientProcedureUri, clientProcedureRequet);
        var clientProcedureActual = JsonConvert.DeserializeObject<ClientProcedureModel>(await clientProcedureResponse.Content.ReadAsStringAsync());
        Assert.That(clientProcedureActual, Is.Not.Null);

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
    public async Task ShouldUploadInvoiceFilesReturnOk()
    {
        var procedureRequest = CreateProcedureApiRequest;
        var procedureResponse = await _client.PostAsJsonAsync(_procedureUri, procedureRequest);
        var procedureContent = await procedureResponse.Content.ReadAsStringAsync();
        var procedureActual = JsonConvert.DeserializeObject<ProcedureModel>(procedureContent);
        Assert.That(procedureActual, Is.Not.Null);

        var clientRequet = CreateClientApiRequest;
        var clientResponse = await _client.PostAsJsonAsync(_clientUri, clientRequet);
        var content = await clientResponse.Content.ReadAsStringAsync();
        var clientActual = JsonConvert.DeserializeObject<ClientModel>(content);
        Assert.That(clientActual, Is.Not.Null);

        var clientProcedureRequet = new CreateClientProcedureApiRequest
        {
            ClientId = clientActual.Id,
            ProcedureId = procedureActual.Id
        };
        var clientProcedureResponse = await _client.PostAsJsonAsync(_clientProcedureUri, clientProcedureRequet);
        var clientProcedureContent = await clientProcedureResponse.Content.ReadAsStringAsync();
        var clientProcedureActual = JsonConvert.DeserializeObject<ClientProcedureModel>(clientProcedureContent);
        Assert.That(clientProcedureActual, Is.Not.Null);

        var invoiceRequest = CreateInvoiceApiRequest(clientActual.Id, clientProcedureActual.Id);
        var invoiceResponse = await _client.PostAsJsonAsync(_uri, invoiceRequest);
        var invoiceContent = await invoiceResponse.Content.ReadAsStringAsync();
        var invoiceActual = JsonConvert.DeserializeObject<InvoiceModel>(invoiceContent);
        Assert.That(invoiceActual, Is.Not.Null);

        var request = InvoiceFormData(invoiceActual.Id.ToString());
        var response = await _client.PostAsync($"{_uri}/upload-invoice-files", request);
        var actual = await response.Content.ReadAsStringAsync();
        Assert.That(!string.IsNullOrEmpty(actual), Is.True);
    }

    [Test]
    [Order(3)]
    public async Task ShouldGetInvoiceFilesReturnOk()
    {
        var procedureRequest = CreateProcedureApiRequest;
        var procedureResponse = await _client.PostAsJsonAsync(_procedureUri, procedureRequest);
        var procedureContent = await procedureResponse.Content.ReadAsStringAsync();
        var procedureActual = JsonConvert.DeserializeObject<ProcedureModel>(procedureContent);
        Assert.That(procedureActual, Is.Not.Null);

        var clientRequet = CreateClientApiRequest;
        var clientResponse = await _client.PostAsJsonAsync(_clientUri, clientRequet);
        var content = await clientResponse.Content.ReadAsStringAsync();
        var clientActual = JsonConvert.DeserializeObject<ClientModel>(content);
        Assert.That(clientActual, Is.Not.Null);

        var clientProcedureRequet = new CreateClientProcedureApiRequest
        {
            ClientId = clientActual.Id,
            ProcedureId = procedureActual.Id
        };
        var clientProcedureResponse = await _client.PostAsJsonAsync(_clientProcedureUri, clientProcedureRequet);
        var clientProcedureContent = await clientProcedureResponse.Content.ReadAsStringAsync();
        var clientProcedureActual = JsonConvert.DeserializeObject<ClientProcedureModel>(clientProcedureContent);
        Assert.That(clientProcedureActual, Is.Not.Null);

        var invoiceRequest = CreateInvoiceApiRequest(clientActual.Id, clientProcedureActual.Id);
        var invoiceResponse = await _client.PostAsJsonAsync(_uri, invoiceRequest);
        var invoiceContent = await invoiceResponse.Content.ReadAsStringAsync();
        var invoiceActual = JsonConvert.DeserializeObject<InvoiceModel>(invoiceContent);
        Assert.That(invoiceActual, Is.Not.Null);

        var request = InvoiceFormDataWithPdf(invoiceActual.Id.ToString());
        var fileResponse = await _client.PostAsync($"{_uri}/upload-invoice-files", request);
        var fileContent = await fileResponse.Content.ReadAsStringAsync();
        var fileActual = JsonConvert.DeserializeObject<IEnumerable<string>>(fileContent);
        Assert.That(fileActual, Is.Not.Empty);

        var response = await _client.GetAsync($"{_uri}/preview-invoice-files/{invoiceActual.Id}");
        var previewContent = await response.Content.ReadAsStringAsync();
        var actual = JsonConvert.DeserializeObject<IEnumerable<FileModel>>(previewContent);
        Assert.That(actual, Is.Not.Empty);
    }
}
