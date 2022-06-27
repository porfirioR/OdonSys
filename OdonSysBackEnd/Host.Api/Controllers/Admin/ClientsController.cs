using AutoMapper;
using Contract.Admin.Clients;
using Host.Api.Models.Clients;
using Host.Api.Models.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Host.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ClientsController : OdonSysBaseController
    {
        private readonly IMapper _mapper;
        private readonly IClientManager _clientManager;

        public ClientsController(IMapper mapper, IClientManager clientManager)
        {
            _mapper = mapper;
            _clientManager = clientManager;
        }

        [HttpPost]
        public async Task<ClientModel> Create([FromBody] CreateClientApiRequest apiRequest)
        {
            var user = _mapper.Map<CreateClientRequest>(apiRequest);
            var model = await _clientManager.CreateAsync(user);
            return model;
        }

        [HttpPut]
        public async Task<ClientModel> Update([FromBody] UpdateClientApiRequest apiRequest)
        {
            var user = _mapper.Map<UpdateClientRequest>(apiRequest);
            var model = await _clientManager.UpdateAsync(user);
            return model;
        }

        [HttpGet]
        public async Task<IEnumerable<ClientModel>> GetAll()
        {
            var model = await _clientManager.GetAllAsync();
            return model;
        }

        [HttpGet("{id}")]
        public async Task<ClientModel> GetById(string id)
        {
            var model = await _clientManager.GetByIdAsync(id);
            return model;
        }

        [HttpGet("document/{documentId}")]
        public async Task<ClientModel> GetByDocumentAsync(string documentId)
        {
            var model = await _clientManager.GetByDocumentAsync(documentId);
            return model;
        }

        [HttpDelete("{id}")]
        public async Task<ClientModel> Delete(string id)
        {
            var model = await _clientManager.DeleteAsync(id);
            return model;
        }

        [HttpPatch("{id}")]
        public async Task<ClientModel> PatchClient(string id, [FromBody] JsonPatchDocument patchDocument)
        {
            if (patchDocument == null) throw new Exception(JsonConvert.SerializeObject(new ApiException(400, "Valor invalido", "No puede ser null.")));
            var clientModel = await _clientManager.GetByIdAsync(id);
            patchDocument.ApplyTo(clientModel);
            if (!ModelState.IsValid)
            {
                throw new Exception(JsonConvert.SerializeObject(new ApiException(400, "Valor invalido", "Valor invalido.")));
            }
            var model = await _clientManager.UpdateAsync(clientModel);
            return model;
        }

        [HttpGet("patients")]
        public async Task<IEnumerable<ClientModel>> GetPatientsByDoctorId()
        {
            var id = UserId;
            var model = await _clientManager.GetClientsByDoctorIdAsync(id);
            return model;
        }

    }
}
