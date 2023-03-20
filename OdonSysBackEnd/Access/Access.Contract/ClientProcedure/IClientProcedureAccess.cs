using Access.Contract.Procedure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Access.Contract.ClientProcedure
{
    public interface IClientProcedureAccess
    {
        Task<IEnumerable<ProcedureAccessModel>> GetProceduresByUserIdAsync(string id);
        Task<ProcedureAccessModel> CreateUserProcedureAsync(UpsertUserProcedureAccessRequest accessRequest);
        Task<ProcedureAccessModel> UpdateUserProcedureAsync(UpsertUserProcedureAccessRequest accessRequest);
        Task<ProcedureAccessModel> DeleteUserProcedureAsync(string userId, string procedureId);
        Task<bool> CheckExistsUserProcedureAsync(string userId, string procedureId);
    }
}
