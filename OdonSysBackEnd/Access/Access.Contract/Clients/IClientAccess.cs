using System.Collections.Generic;
using System.Threading.Tasks;

namespace Access.Contract.Clients
{
    public interface IClientAccess
    {
        Task<ClientAccessResponse> CreateClientAsync(CreateClientAccessRequest accessRequest);
        Task<ClientAccessResponse> UpdateClientAsync(UpdateClientAccessRequest accessRequest);
        Task<ClientAccessResponse> PatchClientAsync(PatchClientAccessRequest accessRequest);
        Task<IEnumerable<ClientAccessResponse>> GetAllAsync();
        Task<ClientAccessResponse> GetByIdAsync(string id, bool active);
        Task DeleteAsync(string id);
    }
}
