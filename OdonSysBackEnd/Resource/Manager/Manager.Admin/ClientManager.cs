using Access.Contract.Clients;
using AutoMapper;
using Contract.Admin.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Admin
{
    internal class ClientManager : IClientManager
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
            return _mapper.Map<ClientModel>(accessModel);
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

        public async Task<IEnumerable<ClientModel>> GetClientsByDoctorIdAsync(string id, string userName)
        {
            var accessModel = await _clientAccess.GetClientsByDoctorIdAsync(id, userName);
            return _mapper.Map<IEnumerable<ClientModel>>(accessModel);
        }
    }
}
