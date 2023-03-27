using Contract.Workspace.ClientProcedures;
using Contract.Workspace.Procedures;
using Host.Api.Models.Auth;
using Host.Api.Models.ClientProcedures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Host.Api.Controllers.Procedure
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientProcedureController : OdonSysBaseController
    {
        private readonly IProcedureManager _procedureManager;

        public ClientProcedureController(IProcedureManager procedureManager)
        {
            _procedureManager = procedureManager;
        }

        [HttpPost]
        [Authorize(Policy = Policy.CanUpdateDoctor)]
        public async Task<ProcedureModel> Create([FromBody] CreateClientProcedureApiRequest apiRequest)
        {
            var userId = UserId;
            var exists = await _procedureManager.CheckExistsUserProcedureAsync(userId, apiRequest.ProcedureId);
            if (exists)
            {
                throw new KeyNotFoundException($"Ya existe la relación entre {userId}, y {apiRequest.ProcedureId}");
            }
            var request = new CreateClientProcedureRequest(userId, apiRequest.ClientId, apiRequest.ProcedureId, apiRequest.Price, apiRequest.Anhestesia);
            var model = await _procedureManager.CreateClientProcedureAsync(request);

            return model;
        }

        [HttpPut]
        [Authorize(Policy = Policy.CanUpdateDoctor)]
        public async Task<ProcedureModel> Update([FromBody] UpdateClientProcedureApiRequest apiRequest)
        {
            var userId = UserId;
            var exists = await _procedureManager.CheckExistsUserProcedureAsync(userId, apiRequest.ProcedureId);
            if (!exists)
            {
                throw new KeyNotFoundException($"No existe la relación entre {userId}, y {apiRequest.ProcedureId}");
            }
            var request = new UpdateClientProcedureRequest(apiRequest.UserClientId, apiRequest.ProcedureId, apiRequest.Price, apiRequest.Status);
            var response = await _procedureManager.UpdateClientProcedureAsync(request);
            return response;
        }

        [HttpGet("all-procedures")]
        public async Task<IEnumerable<ProcedureModel>> GetAll()
        {
            var response = await _procedureManager.GetAllAsync();
            return response;
        }

        [HttpGet("my-procedures")]
        public async Task<IEnumerable<ProcedureModel>> GetProceduresByUser()
        {
            var response = await _procedureManager.GetProceduresByUserIdAsync(UserId);
            return response;
        }

    }
}
