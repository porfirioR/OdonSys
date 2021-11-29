using System.Collections.Generic;
using System.Threading.Tasks;

namespace Resources.Contract.Clients
{
    public interface IClientManager
    {
        Task<ClientAdminModel> Create(CreateClientRequest request);
        Task Delete(string id);
        Task<IEnumerable<ClientAdminModel>> GetAll();
        Task<ClientAdminModel> GetById(string id);
    }
}
