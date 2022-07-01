using System.Collections.Generic;
using System.Threading.Tasks;

namespace Access.Contract.Clients
{
    public interface IClientAccess
    {
        Task<ClientAccessModel> CreateClientAsync(CreateClientAccessRequest accessRequest);
        Task<ClientAccessModel> UpdateClientAsync(UpdateClientAccessRequest accessRequest);
        Task<IEnumerable<ClientAccessModel>> GetAllAsync();
        Task<ClientAccessModel> GetByIdAsync(string id);
        Task<ClientAccessModel> GetByDocumentAsync(string document);
        Task<IEnumerable<ClientAccessModel>> GetClientsByDoctorIdAsync(string id, string userName);
        Task<ClientAccessModel> DeleteAsync(string id);
    }
}
