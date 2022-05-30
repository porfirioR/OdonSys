using AutoMapper;
using Contract.Procedure.Procedures;
using Host.Api.Models.Procedures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Host.Api.Controllers.Procedure
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProcedureController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProcedureManager _procedureManager;
        public ProcedureController(IMapper mapper, IProcedureManager procedureManager)
        {
            _mapper = mapper;
            _procedureManager = procedureManager;
        }

        [HttpPost]
        public async Task<ProcedureModel> Create([FromBody] CreateProcedureApiRequest apiRequest)
        {
            var request = _mapper.Map<CreateProcedureRequest>(apiRequest);
            var model = await _procedureManager.CreateAsync(request);
            return model;
        }

        [HttpPut]
        public async Task<ProcedureModel> Update([FromBody] UpdateProcedureApiRequest apiRequest)
        {
            var request = _mapper.Map<UpdateProcedureRequest>(apiRequest);
            var response = await _procedureManager.UpdateAsync(request);
            return response;
        }
        [HttpGet]
        public async Task<IEnumerable<ProcedureModel>> GetAll()
        {
            var response = await _procedureManager.GetAllAsync();
            return response;
        }

        [HttpGet("{id}/{active}")]
        public async Task<ProcedureModel> GetById(string id, bool active = true)
        {
            var response = await _procedureManager.GetByIdAsync(id, active);
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<ProcedureModel> Delete(string id)
        {
            return await _procedureManager.DeleteAsync(id);
        }

        [HttpPost("restore/{id}")]
        public async Task<ProcedureModel> Resotre(string id)
        {
            return await _procedureManager.RestoreAsync(id);
        }
    }
}
