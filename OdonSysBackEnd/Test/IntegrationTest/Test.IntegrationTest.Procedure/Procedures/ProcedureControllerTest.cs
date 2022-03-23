using Contract.Procedure.Procedures;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AcceptanceTest.Host.Api.Procedures
{
    internal partial class ProcedureControllerTest : TestBase
    {
        private readonly string _uri = "api/procedure";

        [Test]
        [Order(1)]
        public async Task CreateProcedureReturnOk()
        {
            var request = CreateProcedureApiRequest;
            var response = await _client.PostAsJsonAsync(_uri, request);

            var model = JsonConvert.DeserializeObject<ProcedureModel>(await response.Content.ReadAsStringAsync());

            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
            Assert.That(request.Name, Is.EqualTo(model.Name));
            Assert.That(request.Description, Is.EqualTo(model.Description));
            Assert.That(request.EstimatedSessions, Is.EqualTo(model.EstimatedSessions));
            Assert.That(request.ProcedureTeeth.Count(), Is.EqualTo(model.ProcedureTeeth.Count()));
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

            var model = JsonConvert.DeserializeObject<ProcedureModel>(await response.Content.ReadAsStringAsync());

            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
            Assert.That(request.Id, Is.EqualTo(model.Id));
            Assert.That(request.Description, Is.EqualTo(model.Description));
            Assert.That(request.ProcedureTeeth.Count(), Is.EqualTo(model.ProcedureTeeth.Count()));
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

            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
            Assert.That(createModel.Id, Is.EqualTo(model.Id));
            Assert.That(createModel.Description, Is.EqualTo(model.Description));
        }

        [Test]
        [Order(4)]
        public async Task GetAllProcedureReturnOk()
        {
            var createRequest = CreateProcedureApiRequest;
            await _client.PostAsJsonAsync(_uri, createRequest);

            var response = await _client.GetAsync(_uri);
            var model = JsonConvert.DeserializeObject<IEnumerable<ProcedureModel>>(await response.Content.ReadAsStringAsync());

            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
            CollectionAssert.IsNotEmpty(model);
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

            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
            Assert.That(createModel.Id, Is.EqualTo(model.Id));
            Assert.That(createModel.Description, Is.EqualTo(model.Description));
            Assert.AreNotEqual(createModel.Active, Is.EqualTo(model.Active));
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

            Assert.That(HttpStatusCode.OK, Is.EqualTo(deleteResponse.StatusCode));
            Assert.That(deleteModel.Id, Is.EqualTo(model.Id));
            Assert.That(deleteModel.Description, Is.EqualTo(model.Description));
            Assert.That(deleteModel.Active, Is.EqualTo(model.Active));
        }
    }
}