using Contract.Workspace.Procedures;
using Host.Api.Models.Auth;
using Host.Api.Models.Procedures;
using Host.Api.Models.UserProcedures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Host.Api.Controllers.Procedure
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserProcedureController : OdonSysBaseController
    {
        private readonly IProcedureManager _procedureManager;

        public UserProcedureController(IProcedureManager procedureManager)
        {
            _procedureManager = procedureManager;
        }

        [HttpPost]
        [Authorize(Policy = Policy.CanUpdateDoctor)]
        public async Task<ProcedureModel> Create([FromBody] CreateUserProcedureApiRequest apiRequest)
        {
            var userId = UserId;
            var exists = await _procedureManager.CheckExistsUserProcedureAsync(userId, apiRequest.ProcedureId);
            if (exists)
            {
                throw new KeyNotFoundException($"Ya existe la relación entre {userId}, y {apiRequest.ProcedureId}");
            }
            var request = new UpsertUserProcedureRequest(userId, apiRequest.ProcedureId, apiRequest.Price);
            var model = await _procedureManager.CreateUserProcedureAsync(request);
            return model;
        }

        [HttpPut]
        [Authorize(Policy = Policy.CanUpdateDoctor)]
        public async Task<ProcedureModel> Update([FromBody] UpdateUserProcedureApiRequest apiRequest)
        {
            var userId = UserId;
            var exists = await _procedureManager.CheckExistsUserProcedureAsync(userId, apiRequest.ProcedureId);
            if (!exists)
            {
                throw new KeyNotFoundException($"No existe la relación entre {userId}, y {apiRequest.ProcedureId}");
            }
            var request = new UpsertUserProcedureRequest(userId, apiRequest.ProcedureId, apiRequest.Price);
            var response = await _procedureManager.UpdateUserProcedureAsync(request);
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

        [HttpDelete("{procedureId}")]
        [Authorize(Policy = Policy.CanUpdateDoctor)]
        public async Task<ProcedureModel> Delete(string procedureId)
        {
            return await _procedureManager.DeleteUserProcedureAsync(UserId, procedureId);
        }
    }
}
