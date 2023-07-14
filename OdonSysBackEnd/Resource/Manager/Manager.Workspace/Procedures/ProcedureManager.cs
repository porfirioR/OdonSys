using Access.Contract.ClientProcedures;
using Access.Contract.Procedures;
using Access.Contract.Users;
using Contract.Workspace.ClientProcedures;
using Contract.Workspace.Procedures;

namespace Manager.Workspace.Procedures
{
    internal sealed class ProcedureManager : IProcedureManager
    {
        private readonly IProcedureAccess _procedureAccess;
        private readonly IClientProcedureAccess _clientProcedureAccess;
        private readonly IUserDataAccess _userDataAccess;
        private readonly IProcedureManagerBuilder _procedureManagerBuilder;

        public ProcedureManager(
            IProcedureAccess procedureAccess,
            IClientProcedureAccess clientProcedureAccess,
            IUserDataAccess userDataAccess,
            IProcedureManagerBuilder procedureManagerBuilder)
        {
            _procedureAccess = procedureAccess;
            _clientProcedureAccess = clientProcedureAccess;
            _userDataAccess = userDataAccess;
            _procedureManagerBuilder = procedureManagerBuilder;
        }

        public async Task<ProcedureModel> CreateAsync(CreateProcedureRequest request)
        {
            var accessRequest = _procedureManagerBuilder.MapCreateProcedureRequestToCreateProcedureAccessRequest(request);
            var accessModel = await _procedureAccess.CreateAsync(accessRequest);
            return _procedureManagerBuilder.MapProcedureAccessModelToProcedureModel(accessModel);
        }

        public async Task<ProcedureModel> DeleteAsync(string id)
        {
            var accessModel = await _procedureAccess.DeleteAsync(id);
            return _procedureManagerBuilder.MapProcedureAccessModelToProcedureModel(accessModel);
        }

        public async Task<IEnumerable<ProcedureModel>> GetAllAsync()
        {
            var accessModelList = await _procedureAccess.GetAllAsync();
            return accessModelList.Select(_procedureManagerBuilder.MapProcedureAccessModelToProcedureModel);
        }

        public async Task<ProcedureModel> GetByIdAsync(string id, bool active)
        {
            var accessModel = await _procedureAccess.GetByIdAsync(id, active);
            return _procedureManagerBuilder.MapProcedureAccessModelToProcedureModel(accessModel);
        }

        public async Task<ProcedureModel> UpdateAsync(UpdateProcedureRequest request)
        {
            var accessRequest = _procedureManagerBuilder.MapUpdateProcedureRequestToUpdateProcedureAccessRequest(request);
            var accessModel = await _procedureAccess.UpdateAsync(accessRequest);
            return _procedureManagerBuilder.MapProcedureAccessModelToProcedureModel(accessModel);
        }

        public async Task<bool> ValidateIdNameAsync(string name)
        {
            return await _procedureAccess.ValidateIdNameAsync(name);
        }

        //public async Task<IEnumerable<string>> ValidateProcedureTeethAsync(IEnumerable<string> ids)
        //{
        //    return await _procedureAccess.ValidateProcedureTeethAsync(ids);
        //}

        public async Task<IEnumerable<ProcedureModel>> GetProceduresByUserIdAsync(string id)
        {
            var userClientList = (await _userDataAccess.GetUserClientsByUserIdAsync(id)).Select(x => x.Id);
            var clientProceduresAccessResponse = await _clientProcedureAccess.GetClientProceduresByUserClientIdAsync(userClientList);
            var allProcedures = await _procedureAccess.GetAllAsync();
            var myProcedureIds = clientProceduresAccessResponse.Select(x => x.ProcedureId);
            var procedures = allProcedures.Where(x => myProcedureIds.Contains(x.Id));
            return procedures.Select(_procedureManagerBuilder.MapProcedureAccessModelToProcedureModel);
        }

        public async Task<ClientProcedureModel> CreateClientProcedureAsync(CreateClientProcedureRequest request)
        {
            var userClient = await _userDataAccess.GetUserClientAsync(_procedureManagerBuilder.MapCreateClientProcedureRequestToUserClientAccessRequest(request));
            userClient ??= await _userDataAccess.CreateUserClientAsync(_procedureManagerBuilder.MapCreateClientProcedureRequestToUserClientAccessRequest(request));

            var accessRequest = new CreateClientProcedureAccessRequest(userClient.Id.ToString(), request.ProcedureId);
            var accessModel = await _clientProcedureAccess.CreateClientProcedureAsync(accessRequest);
            return _procedureManagerBuilder.MapClientProcedureAccessModelToClientProcedureModel(accessModel);
        }

        public async Task<ClientProcedureModel> UpdateClientProcedureAsync(UpdateClientProcedureRequest request)
        {
            var accessRequest = _procedureManagerBuilder.MapUpdateClientProcedureRequestToUpdateClientProcedureAccessRequest(request);
            var accessModel = await _clientProcedureAccess.UpdateClientProcedureAsync(accessRequest);
            return _procedureManagerBuilder.MapClientProcedureAccessModelToClientProcedureModel(accessModel);
        }

        public async Task<bool> CheckExistsClientProcedureAsync(string userClientId, string procedureId)
        {
            return await _clientProcedureAccess.CheckExistsClientProcedureAsync(userClientId, procedureId);
        }

        public async Task<bool> CheckExistsClientProcedureAsync(string clientProcedureId)
        {
            return await _clientProcedureAccess.CheckExistsClientProcedureAsync(clientProcedureId);
        }

        public async Task<ProcedureModel> GetProcedureByClientProcedureIdAsync(string clientProcedureId)
        {
            var clientProcedureAccessModel = await _clientProcedureAccess.GetClientProcedureByIdAsync(clientProcedureId);
            var model = await GetByIdAsync(clientProcedureAccessModel.ProcedureId, true);
            return model;
        }
    }
}
