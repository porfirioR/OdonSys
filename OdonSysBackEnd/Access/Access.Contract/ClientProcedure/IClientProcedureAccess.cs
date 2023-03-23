using Access.Contract.Procedure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Access.Contract.ClientProcedure
{
    public interface IClientProcedureAccess
    {
        Task<IEnumerable<ProcedureAccessModel>> GetProceduresByUserIdAsync(string id);
        Task<ClientProcedureAccessModel> CreateClientProcedureAsync(CreateClientProcedureAccessRequest accessRequest);
        Task<ProcedureAccessModel> UpdateClientProcedureAsync(UpdateClientProcedureAccessRequest accessRequest);
        Task<bool> CheckExistsUserProcedureAsync(string userId, string procedureId);
    }
}
