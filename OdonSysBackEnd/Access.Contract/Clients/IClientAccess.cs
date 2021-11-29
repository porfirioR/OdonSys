using System.Threading.Tasks;

namespace Access.Contract.Clients
{
    public interface IClientAccess
    {
        Task<ClientAccessResponse> CreateClientAsync(CreateClientAccessRequest accessRequest);
        Task<ClientAccessResponse> UpadateClientAsync(UpdateClientAccessRequest accessRequest);
        Task<ClientAccessResponse> GetAllAsync();
        Task<ClientAccessResponse> GetByIdAsync(int id);
    }
}
