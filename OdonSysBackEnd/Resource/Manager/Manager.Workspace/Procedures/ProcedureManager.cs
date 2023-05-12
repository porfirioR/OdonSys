using Access.Contract.ClientProcedure;
using Access.Contract.Procedure;
using Access.Contract.Users;
using AutoMapper;
using Contract.Workspace.ClientProcedures;
using Contract.Workspace.Procedures;

namespace Manager.Workspace.Procedures
{
    internal class ProcedureManager : IProcedureManager
    {
        private readonly IProcedureAccess _procedureAccess;
        private readonly IClientProcedureAccess _clientProcedureAccess;
        private readonly IUserDataAccess _userDataAccess;
        private readonly IMapper _mapper;

        public ProcedureManager(IProcedureAccess procedureAccess, IMapper mapper, IClientProcedureAccess clientProcedureAccess, IUserDataAccess userDataAccess)
        {
            _procedureAccess = procedureAccess;
            _mapper = mapper;
            _clientProcedureAccess = clientProcedureAccess;
            _userDataAccess = userDataAccess;
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

        public async Task<IEnumerable<ProcedureModel>> GetProceduresByUserIdAsync(string id)
        {
            var userClientList = (await _userDataAccess.GetUserClientsByUserIdAsync(id)).Select(x => x.Id);
            var clientProceduresAccessResponse = await _clientProcedureAccess.GetClientProceduresByUserClientIdAsync(userClientList);
            var allProcedures = await _procedureAccess.GetAllAsync();
            var myProcedureIds = clientProceduresAccessResponse.Select(x => x.ProcedureId);
            var procedures = allProcedures.Where(x => myProcedureIds.Contains(x.Id));
            return _mapper.Map<IEnumerable<ProcedureModel>>(procedures);
        }

        public async Task<ClientProcedureModel> CreateClientProcedureAsync(CreateClientProcedureRequest request)
        {
            var userClient = await _userDataAccess.GetUserClientAsync(new UserClientAccessRequest(request.ClientId, request.UserId));
            userClient ??= await _userDataAccess.CreateUserClientAsync(new UserClientAccessRequest(request.ClientId, request.UserId));
            var accessRequest = new CreateClientProcedureAccessRequest(userClient.Id.ToString(), request.ProcedureId);
            var accessResponse = await _clientProcedureAccess.CreateClientProcedureAsync(accessRequest);

            return new ClientProcedureModel(
                accessResponse.Id,
                accessResponse.ProcedureId,
                accessResponse.UserClientId
            );
        }

        public async Task<ClientProcedureModel> UpdateClientProcedureAsync(UpdateClientProcedureRequest request)
        {
            var accessRequest = new UpdateClientProcedureAccessRequest(request.UserClientId, request.ProcedureId);
            var accessResponse = await _clientProcedureAccess.UpdateClientProcedureAsync(accessRequest);
            return new ClientProcedureModel(
                accessResponse.Id,
                accessResponse.ProcedureId,
                accessResponse.UserClientId
            );
        }

        public async Task<bool> CheckExistsClientProcedureAsync(string userClientId, string procedureId)
        {
            return await _clientProcedureAccess.CheckExistsClientProcedureAsync(userClientId, procedureId);
        }
    }
}
