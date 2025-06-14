using Contract.Workspace.Procedures;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Json;

namespace AcceptanceTest.Host.Api.Procedures;

internal partial class ProcedureControllerTest : TestBase
{
    private readonly string _uri = "api/procedure";

    [Test]
    [Order(1)]
    public async Task CreateProcedureReturnOk()
    {
        var request = CreateProcedureApiRequest;
        var response = await _client.PostAsJsonAsync(_uri, request);

        var actual = JsonConvert.DeserializeObject<ProcedureModel>(await response.Content.ReadAsStringAsync());
        Assert.Multiple(() =>
        {
            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
            Assert.That(request.Name, Is.EqualTo(actual.Name));
            Assert.That(request.Description, Is.EqualTo(actual.Description));
            Assert.That(request.Price, Is.EqualTo(actual.Price));
            //Assert.That(request.ProcedureTeeth.Count(), Is.EqualTo(actual.ProcedureTeeth.Count()));
        });
    }

    [Test]
    [Order(2)]
    public async Task UpdateProcedureReturnOk()
    {
        var createRequest = CreateProcedureApiRequest;
        var createResponse = await _client.PostAsJsonAsync(_uri, createRequest);
        var createModel = JsonConvert.DeserializeObject<ProcedureModel>(await createResponse.Content.ReadAsStringAsync());

        var request = UpdateProcedureApiRequest(createModel.Id);
        var response = await _client.PutAsJsonAsync(_uri, request);

        var actual = JsonConvert.DeserializeObject<ProcedureModel>(await response.Content.ReadAsStringAsync());
        Assert.Multiple(() =>
        {
            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
            Assert.That(request.Id, Is.EqualTo(actual.Id));
            Assert.That(request.Description, Is.EqualTo(actual.Description));
            Assert.That(request.ProcedureTeeth.Count(), Is.EqualTo(actual.ProcedureTeeth.Count()));
        });
    }

    [Test]
    [Order(3)]
    public async Task GetProcedureByIdReturnOk()
    {
        var createRequest = CreateProcedureApiRequest;
        var createResponse = await _client.PostAsJsonAsync(_uri, createRequest);
        var createModel = JsonConvert.DeserializeObject<ProcedureModel>(await createResponse.Content.ReadAsStringAsync());

        var response = await _client.GetAsync($"{_uri}/{createModel.Id}/{true}");
        var model = JsonConvert.DeserializeObject<ProcedureModel>(await response.Content.ReadAsStringAsync());

        Assert.Multiple(() =>
        {
            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
            Assert.That(createModel.Id, Is.EqualTo(model.Id));
            Assert.That(createModel.Description, Is.EqualTo(model.Description));
        });
    }

    [Test]
    [Order(4)]
    public async Task GetAllProcedureReturnOk()
    {
        var createRequest = CreateProcedureApiRequest;
        await _client.PostAsJsonAsync(_uri, createRequest);

        var response = await _client.GetAsync(_uri);
        var model = JsonConvert.DeserializeObject<IEnumerable<ProcedureModel>>(await response.Content.ReadAsStringAsync());

        Assert.Multiple(() =>
        {
            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
            Assert.That(model, Is.Not.Empty);
        });
    }

    [Test]
    [Order(5)]
    public async Task DeleteProcedureReturnOk()
    {
        var createRequest = CreateProcedureApiRequest;
        var createResponse = await _client.PostAsJsonAsync(_uri, createRequest);
        var createModel = JsonConvert.DeserializeObject<ProcedureModel>(await createResponse.Content.ReadAsStringAsync());

        var response = await _client.DeleteAsync($"{_uri}/{createModel.Id}");

        var model = JsonConvert.DeserializeObject<ProcedureModel>(await response.Content.ReadAsStringAsync());

        Assert.Multiple(() =>
        {
            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
            Assert.That(createModel.Id, Is.EqualTo(model.Id));
            Assert.That(createModel.Description, Is.EqualTo(model.Description));
            Assert.That(createModel.Active, Is.Not.EqualTo(model.Active));
        });
    }

    [Test]
    [Order(6)]
    public async Task RestoreProcedureReturnOk()
    {
        var createRequest = CreateProcedureApiRequest;
        var createResponse = await _client.PostAsJsonAsync(_uri, createRequest);
        var createModel = JsonConvert.DeserializeObject<ProcedureModel>(await createResponse.Content.ReadAsStringAsync());

        var deleteResponse = await _client.DeleteAsync($"{_uri}/{createModel.Id}");
        var deleteModel = JsonConvert.DeserializeObject<ProcedureModel>(await deleteResponse.Content.ReadAsStringAsync());

        var restoreResponse = await _client.PostAsync($"{_uri}/restore/{deleteModel.Id}", null);
        var model = JsonConvert.DeserializeObject<ProcedureModel>(await restoreResponse.Content.ReadAsStringAsync());

        Assert.Multiple(() =>
        {
            Assert.That(HttpStatusCode.OK, Is.EqualTo(deleteResponse.StatusCode));
            Assert.That(deleteModel.Id, Is.EqualTo(model.Id));
            Assert.That(deleteModel.Description, Is.EqualTo(model.Description));
            Assert.That(deleteModel.Active, Is.Not.EqualTo(model.Active));
        });
    }
}