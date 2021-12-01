using System.Collections.Generic;
using System.Threading.Tasks;

namespace Access.Contract.Clients
{
    public interface IClientAccess
    {
        Task<ClientAccessResponse> CreateClientAsync(CreateClientAccessRequest accessRequest);
        Task<ClientAccessResponse> UpadateClientAsync(UpdateClientAccessRequest accessRequest);
        Task<IEnumerable<ClientAccessResponse>> GetAllAsync();
        Task<ClientAccessResponse> GetByIdAsync(string id);
        Task DeleteAsync(string id);
    }
}
