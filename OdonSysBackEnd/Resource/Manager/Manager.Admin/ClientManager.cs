using Access.Contract.Clients;
using AutoMapper;
using Contract.Admin.Clients;
using Contract.Admin.Users;

namespace Manager.Admin
{
    internal sealed class ClientManager : IClientManager
    {
        private readonly IClientAccess _clientAccess;
        private readonly IMapper _mapper;

        public ClientManager(IClientAccess clientAccess, IMapper mapper)
        {
            _clientAccess = clientAccess;
            _mapper = mapper;
        }

        public async Task<ClientModel> CreateAsync(CreateClientRequest request)
        {
            var accessRequest = _mapper.Map<CreateClientAccessRequest>(request);
            var accessModel = await _clientAccess.CreateClientAsync(accessRequest);
            var allClientsForCurrentDoctor = await AssignClientToUser(new AssignClientRequest(request.UserId, accessModel.Id));
            var currentDoctor = allClientsForCurrentDoctor.SelectMany(x => x.Doctors).Distinct().First(x => x.Id == request.UserId);
            var model = _mapper.Map<ClientModel>(accessModel);
            model.Doctors = new List<DoctorModel>() { currentDoctor };
            return model;
        }

        public async Task<ClientModel> UpdateAsync(UpdateClientRequest request)
        {
            var accessRequest = _mapper.Map<UpdateClientAccessRequest>(request);
            var accessModel = await _clientAccess.UpdateClientAsync(accessRequest);
            return _mapper.Map<ClientModel>(accessModel);
        }

        public async Task<IEnumerable<ClientModel>> GetAllAsync()
        {
            var accessModel = await _clientAccess.GetAllAsync();
            return _mapper.Map<IEnumerable<ClientModel>>(accessModel);
        }

        public async Task<ClientModel> GetByIdAsync(string id)
        {
            var accessModel = await _clientAccess.GetByIdAsync(id);
            return _mapper.Map<ClientModel>(accessModel);
        }

        public async Task<ClientModel> GetByDocumentAsync(string documentId)
        {
            var accessModel = await _clientAccess.GetByDocumentAsync(documentId);
            return _mapper.Map<ClientModel>(accessModel);
        }

        public async Task<ClientModel> DeleteAsync(string id)
        {
            var accessModel = await _clientAccess.DeleteAsync(id);
            return _mapper.Map<ClientModel>(accessModel);
        }

        public async Task<IEnumerable<ClientModel>> GetClientsByUserIdAsync(string id, string userName)
        {
            var accessModel = await _clientAccess.GetClientsByUserIdAsync(id, userName);
            return _mapper.Map<IEnumerable<ClientModel>>(accessModel);
        }

        public async Task<IEnumerable<ClientModel>> AssignClientToUser(AssignClientRequest request)
        {
            var accessRequest = new AssignClientAccessRequest(request.UserId, request.ClientId);
            var accessModel = await _clientAccess.AssignClientToUserAsync(accessRequest);
            return _mapper.Map<IEnumerable<ClientModel>>(accessModel);
        }

        public async Task<bool> IsDuplicateEmailAsync(string email, string id = null)
        {
            return await _clientAccess.IsDuplicateEmailAsync(email, id);
        }

        public Task<bool> IsDuplicateDocumentAsync(string document, string id)
        {
            throw new NotImplementedException();
        }
    }
}
