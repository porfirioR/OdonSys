using AutoMapper;
using Contract.Workspace.Procedures;
using Host.Api.Models.Authorization;
using Host.Api.Models.Error;
using Host.Api.Models.Procedures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Host.Api.Controllers.Procedure
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ProcedureController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProcedureManager _procedureManager;

        public ProcedureController(IMapper mapper, IProcedureManager procedureManager)
        {
            _mapper = mapper;
            _procedureManager = procedureManager;
        }

        [HttpPost]
        [Authorize(Policy = Policy.CanManageProcedure)]
        public async Task<ProcedureModel> Create([FromBody] CreateProcedureApiRequest apiRequest)
        {
            var request = _mapper.Map<CreateProcedureRequest>(apiRequest);
            var model = await _procedureManager.CreateAsync(request);
            return model;
        }

        [HttpPut]
        [Authorize(Policy = Policy.CanManageProcedure)]
        public async Task<ProcedureModel> Update([FromBody] UpdateProcedureApiRequest apiRequest)
        {
            var request = _mapper.Map<UpdateProcedureRequest>(apiRequest);
            var response = await _procedureManager.UpdateAsync(request);
            return response;
        }

        [HttpGet]
        [Authorize(Policy = Policy.CanAccessProcedure)]
        public async Task<IEnumerable<ProcedureModel>> GetAll()
        {
            var response = await _procedureManager.GetAllAsync();
            return response;
        }

        [HttpGet("{id}/{active}")]
        [Authorize(Policy = Policy.CanAccessProcedure)]
        public async Task<ProcedureModel> GetById(string id, bool active = true)
        {
            var response = await _procedureManager.GetByIdAsync(id, active);
            return response;
        }

        // Hard Delete
        [HttpDelete("{id}")]
        [Authorize(Policy = Policy.CanManageProcedure)]
        public async Task<ProcedureModel> Delete(string id)
        {
            return await _procedureManager.DeleteAsync(id);
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = Policy.CanModifyVisibilityProcedure)]
        public async Task<ProcedureModel> PatchProcedure([FromRoute] string id, [FromBody] JsonPatchDocument<UpdateProcedureRequest> patchProcedure)
        {
            if (patchProcedure == null) throw new Exception(JsonConvert.SerializeObject(new ApiException(400, "Valor inválido", "No puede estar vacío.")));
            ProcedureModel response;
            try
            {
                response = await _procedureManager.GetByIdAsync(id, true);
            }
            catch (KeyNotFoundException)
            {
                response = await _procedureManager.GetByIdAsync(id, false);
            }
            var updateProcedureRequest = _mapper.Map<UpdateProcedureRequest>(response);
            patchProcedure.ApplyTo(updateProcedureRequest);
            if (!ModelState.IsValid)
            {
                throw new Exception(JsonConvert.SerializeObject(new ApiException(400, "Valor inválido", "Valor inválido.")));
            }
            var model = await _procedureManager.UpdateAsync(updateProcedureRequest);
            return model;
        }
    }
}
