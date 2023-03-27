using System.Collections.Generic;
using System.Threading.Tasks;

namespace Access.Contract.ClientProcedure
{
    public interface IClientProcedureAccess
    {
        Task<IEnumerable<ClientProcedureAccessModel>> GetClientProceduresByUserClientIdAsync(string id);
        Task<ClientProcedureAccessModel> CreateClientProcedureAsync(CreateClientProcedureAccessRequest accessRequest);
        Task<ClientProcedureAccessModel> UpdateClientProcedureAsync(UpdateClientProcedureAccessRequest accessRequest);
        Task<bool> CheckExistsClientProcedureAsync(string userClientId, string procedureId);
    }
}
