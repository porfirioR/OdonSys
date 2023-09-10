using Contract.Administration.Clients;
using Contract.Administration.Reports;
using Contract.Administration.Roles;
using Host.Api.Contract.Authorization;
using Host.Api.Contract.Clients;
using Host.Api.Contract.Error;
using Host.Api.Contract.MapBuilders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Utilities.Enums;
using Utilities.Extensions;

namespace Host.Api.Controllers.Administration
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public sealed class ClientsController : OdonSysBaseController
    {
        private readonly IClientManager _clientManager;
        private readonly IRoleManager _roleManager;
        private readonly IClientHostBuilder _clientHostBuilder;

        public ClientsController(IClientManager clientManager, IRoleManager roleManager, IClientHostBuilder clientHostBuilder)
        {
            _clientManager = clientManager;
            _roleManager = roleManager;
            _clientHostBuilder = clientHostBuilder;
        }

        [HttpPost]
        [Authorize(Policy = Policy.CanManageClient)]
        public async Task<ClientModel> Create([FromBody] CreateClientApiRequest apiRequest)
        {
            var request = _clientHostBuilder.MapCreateClientApiRequestToCreateClientRequest(apiRequest);
            request.UserId = UserId;
            var model = await _clientManager.CreateAsync(request);
            return model;
        }

        [HttpPut]
        [Authorize(Policy = Policy.CanManageClient)]
        public async Task<ClientModel> Update([FromBody] UpdateClientApiRequest apiRequest)
        {
            var permissions = await _roleManager.GetPermissionsByUserIdAsync(UserId);
            var canFullEdit = permissions.Any(x => x == PermissionName.FullFieldUpdateClients.GetDescription());
            var request = _clientHostBuilder.MapUpdateClientApiRequestToUpdateClientRequest(apiRequest, canFullEdit);
            var model = await _clientManager.UpdateAsync(request);
            return model;
        }

        [HttpGet]
        [Authorize(Policy = Policy.CanAccessClient)]
        public async Task<IEnumerable<ClientModel>> GetAll()
        {
            var model = await _clientManager.GetAllAsync();
            return model;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Policy.CanAccessClient)]
        public async Task<ClientModel> GetById(string id)
        {
            var model = await _clientManager.GetByIdAsync(id);
            return model;
        }

        [HttpGet("document/{documentId}")]
        [Authorize(Policy = Policy.CanAccessClient)]
        public async Task<ClientModel> GetByDocumentAsync(string documentId)
        {
            var model = await _clientManager.GetByDocumentAsync(documentId);
            return model;
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Policy.CanDeleteClient)]
        public async Task<ClientModel> Delete(string id)
        {
            var model = await _clientManager.DeleteAsync(id);
            return model;
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = Policy.CanModifyVisibilityClient)]
        public async Task<ClientModel> PatchClient(string id, [FromBody] JsonPatchDocument<UpdateClientRequest> patchClient)
        {
            if (patchClient == null)
            {
                throw new Exception(JsonConvert.SerializeObject(new ApiException(400, "Valor inválido", "No puede estar vacío.")));
            }
            var clientRequest = _clientHostBuilder.MapClientModelToUpdateClientRequest(await _clientManager.GetByIdAsync(id));
            patchClient.ApplyTo(clientRequest);
            if (!ModelState.IsValid)
            {
                throw new Exception(JsonConvert.SerializeObject(new ApiException(400, "Valor inválido", "Valor inválido.")));
            }
            var model = await _clientManager.UpdateAsync(clientRequest);
            return model;
        }

        [HttpGet("patients")]
        [Authorize(Policy = Policy.CanAccessClient)]
        public async Task<IEnumerable<ClientModel>> GetClientsByUserId()
        {
            var id = UserId;
            var userName = Username;
            var model = await _clientManager.GetClientsByUserIdAsync(id, userName);
            return model;
        }

        [HttpGet("report/{id}")]
        [Authorize(Policy = Policy.CanAccessClient)]
        public async Task<ClientReportModel> GetReportProcedures(string id)
        {
            var model = await _clientManager.GetReportByIdAsync(id);
            return model;
        }

    }
}
