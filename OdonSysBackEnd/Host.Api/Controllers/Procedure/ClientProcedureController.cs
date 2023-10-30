using Contract.Administration.Users;
using Contract.Workspace.ClientProcedures;
using Contract.Workspace.Procedures;
using Host.Api.Contract.Authorization;
using Host.Api.Contract.ClientProcedures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Api.Controllers.Procedure
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ClientProcedureController : OdonSysBaseController
    {
        private readonly IProcedureManager _procedureManager;
        private readonly IUserManager _userManager;

        public ClientProcedureController(IProcedureManager procedureManager, IUserManager userManager)
        {
            _procedureManager = procedureManager;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize(Policy = Policy.CanCreateClientProcedure)]
        public async Task<ClientProcedureModel> Create([FromBody] CreateClientProcedureApiRequest apiRequest)
        {
            var id = string.IsNullOrEmpty(UserId) ? await _userManager.GetInternalUserIdByExternalUserIdAsync(UserIdAadB2C) : UserId;
            var request = new CreateClientProcedureRequest(id, apiRequest.ClientId, apiRequest.ProcedureId);
            var response = await _procedureManager.CreateClientProcedureAsync(request);
            return response;
        }

        [HttpPut]
        [Authorize(Policy = Policy.CanUpdateClientProcedure)]
        public async Task<ClientProcedureModel> Update([FromBody] UpdateClientProcedureApiRequest apiRequest)
        {
            var request = new UpdateClientProcedureRequest(apiRequest.UserClientId, apiRequest.ProcedureId);
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
            var id = string.IsNullOrEmpty(UserId) ? await _userManager.GetInternalUserIdByExternalUserIdAsync(UserIdAadB2C) : UserId;
            var response = await _procedureManager.GetProceduresByUserIdAsync(id);
            return response;
        }

    }
}
