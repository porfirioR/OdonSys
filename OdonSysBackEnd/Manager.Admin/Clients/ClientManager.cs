using Access.Contract.Clients;
using AutoMapper;
using Resources.Contract.Clients;
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

        public Task<ClientAdminModel> Create(CreateClientRequest user)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ClientAdminModel>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<ClientAdminModel> GetById(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
