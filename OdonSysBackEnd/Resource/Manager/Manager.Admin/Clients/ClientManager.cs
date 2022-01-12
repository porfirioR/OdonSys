using Access.Contract.Clients;
using AutoMapper;
using Contract.Admin.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Admin.Clients
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
            var accessResponse = await _clientAccess.CreateClientAsync(accessRequest);
            return _mapper.Map<ClientModel>(accessResponse);
        }

        public async Task<ClientModel> UpdateAsync(UpdateClientRequest request)
        {
            var accessRequest = _mapper.Map<UpdateClientAccessRequest>(request);
            var accessResponse = await _clientAccess.UpdateClientAsync(accessRequest);
            return _mapper.Map<ClientModel>(accessResponse);
        }
        public async Task<ClientModel> UpdateAsync(ClientModel request)
        {
            var accessRequest = _mapper.Map<PatchClientAccessRequest>(request);
            var accessResponse = await _clientAccess.PatchClientAsync(accessRequest);
            return _mapper.Map<ClientModel>(accessResponse);

        }

        public async Task<IEnumerable<ClientModel>> GetAllAsync()
        {
            var accessResponse = await _clientAccess.GetAllAsync();
            return _mapper.Map<IEnumerable<ClientModel>>(accessResponse);
        }

        public async Task<ClientModel> GetByIdAsync(string id, bool active)
        {
            var accessResponse = await _clientAccess.GetByIdAsync(id, active);
            return _mapper.Map<ClientModel>(accessResponse);
        }

        public async Task DeleteAsync(string id)
        {
            await _clientAccess.DeleteAsync(id);
        }

    }
}
