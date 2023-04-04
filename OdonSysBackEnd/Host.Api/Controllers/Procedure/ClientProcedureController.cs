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
        [Authorize(Policy = Policy.CanCreateClientProcedure)]
        public async Task<ClientProcedureModel> Create([FromBody] CreateClientProcedureApiRequest apiRequest)
        {
            var request = new CreateClientProcedureRequest(UserId, apiRequest.ClientId, apiRequest.ProcedureId, apiRequest.Price, apiRequest.Anhestesia);
            var response = await _procedureManager.CreateClientProcedureAsync(request);
            return response;
        }

        [HttpPut]
        [Authorize(Policy = Policy.CanUpdateClientProcedure)]
        public async Task<ClientProcedureModel> Update([FromBody] UpdateClientProcedureApiRequest apiRequest)
        {
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
