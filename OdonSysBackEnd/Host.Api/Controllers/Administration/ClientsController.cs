﻿using AutoMapper;
using Contract.Administration.Clients;
using Host.Api.Models.Auth;
using Host.Api.Models.Clients;
using Host.Api.Models.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Host.Api.Controllers.Administration
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public sealed class ClientsController : OdonSysBaseController
    {
        private readonly IMapper _mapper;
        private readonly IClientManager _clientManager;

        public ClientsController(IMapper mapper, IClientManager clientManager)
        {
            _mapper = mapper;
            _clientManager = clientManager;
        }

        [HttpPost]
        [Authorize(Policy = Policy.CanManageClient)]
        public async Task<ClientModel> Create([FromBody] CreateClientApiRequest apiRequest)
        {
            var request = _mapper.Map<CreateClientRequest>(apiRequest);
            request.UserId = UserId;
            var model = await _clientManager.CreateAsync(request);
            return model;
        }

        [HttpPut]
        [Authorize(Policy = Policy.CanManageClient)]
        public async Task<ClientModel> Update([FromBody] UpdateClientApiRequest apiRequest)
        {
            var request = _mapper.Map<UpdateClientRequest>(apiRequest);
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
            if (patchClient == null) throw new Exception(JsonConvert.SerializeObject(new ApiException(400, "Valor inválido", "No puede estar vacío.")));
            var clientRequest = _mapper.Map<UpdateClientRequest>(await _clientManager.GetByIdAsync(id));
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

    }
}