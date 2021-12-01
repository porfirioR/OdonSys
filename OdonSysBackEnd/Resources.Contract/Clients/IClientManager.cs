using System.Collections.Generic;
using System.Threading.Tasks;

namespace Resources.Contract.Clients
{
    public interface IClientManager
    {
        Task<ClientModel> CreateAsync(CreateClientRequest request);
        Task<ClientModel> UpdateAsync(UpdateClientRequest request);
        Task DeleteAsync(string id);
        Task<IEnumerable<ClientModel>> GetAllAsync();
        Task<ClientModel> GetByIdAsync(string id);
    }
}
