using Access.Contract.Clients;
using System.Threading.Tasks;

namespace Access.Admin.Clients
{
    internal class ClientAccess : IClientAccess
    {
        public ClientAccess()
        {

        }
        public Task<ClientAccessResponse> CreateClientAsync(CreateClientAccessRequest accessRequest)
        {
            throw new System.NotImplementedException();
        }

        public Task<ClientAccessResponse> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<ClientAccessResponse> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ClientAccessResponse> UpadateClientAsync(UpdateClientAccessRequest accessRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}
