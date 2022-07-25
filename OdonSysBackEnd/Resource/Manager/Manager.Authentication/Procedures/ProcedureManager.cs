using Access.Contract.Procedure;
using AutoMapper;
using Contract.Workspace.Procedures;

namespace Manager.Workspace.Procedures
{
    internal class ProcedureManager : IProcedureManager
    {
        private readonly IProcedureAccess _procedureAccess;
        private readonly IMapper _mapper;

        public ProcedureManager(IProcedureAccess procedureAccess, IMapper mapper)
        {
            _procedureAccess = procedureAccess;
            _mapper = mapper;
        }

        public async Task<ProcedureModel> CreateAsync(CreateProcedureRequest request)
        {
            var accessRequest = _mapper.Map<CreateProcedureAccessRequest>(request);
            var accessResponse = await _procedureAccess.CreateAsync(accessRequest);
            return _mapper.Map<ProcedureModel>(accessResponse);
        }

        public async Task<ProcedureModel> DeleteAsync(string id)
        {
            var accessResponse = await _procedureAccess.DeleteAsync(id);
            return _mapper.Map<ProcedureModel>(accessResponse);
        }

        public async Task<ProcedureModel> RestoreAsync(string id)
        {
            var accessResponse = await _procedureAccess.RestoreAsync(id);
            return _mapper.Map<ProcedureModel>(accessResponse);
        }

        public async Task<IEnumerable<ProcedureModel>> GetAllAsync()
        {
            var accessResponse = await _procedureAccess.GetAllAsync();
            return _mapper.Map<IEnumerable<ProcedureModel>>(accessResponse);
        }

        public async Task<ProcedureModel> GetByIdAsync(string id, bool active)
        {
            var accessResponse = await _procedureAccess.GetByIdAsync(id, active);
            return _mapper.Map<ProcedureModel>(accessResponse);
        }

        public async Task<ProcedureModel> UpdateAsync(UpdateProcedureRequest request)
        {
            var accessRequest = _mapper.Map<UpdateProcedureAccessRequest>(request);
            var accessResponse = await _procedureAccess.UpdateAsync(accessRequest);
            return _mapper.Map<ProcedureModel>(accessResponse);
        }

        public async Task<bool> ValidateIdNameAsync(string name)
        {
            return await _procedureAccess.ValidateIdNameAsync(name);
        }

        public async Task<IEnumerable<string>> ValidateProcedureTeethAsync(IEnumerable<string> ids)
        {
            return await _procedureAccess.ValidateProcedureTeethAsync(ids);
        }
    }
}
