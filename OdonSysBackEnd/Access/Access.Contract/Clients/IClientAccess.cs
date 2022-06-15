using System.Collections.Generic;
using System.Threading.Tasks;

namespace Access.Contract.Clients
{
    public interface IClientAccess
    {
        Task<ClientAccessModel> CreateClientAsync(CreateClientAccessRequest accessRequest);
        Task<ClientAccessModel> UpdateClientAsync(UpdateClientAccessRequest accessRequest);
        Task<ClientAccessModel> PatchClientAsync(PatchClientAccessRequest accessRequest);
        Task<IEnumerable<ClientAccessModel>> GetAllAsync();
        Task<ClientAccessModel> GetByIdAsync(string id);
        Task<IEnumerable<ClientAccessModel>> GetClientsByDoctorIdAsync(string id);
        Task DeleteAsync(string id);
    }
}
