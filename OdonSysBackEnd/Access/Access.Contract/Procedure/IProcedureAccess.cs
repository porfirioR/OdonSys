using System.Collections.Generic;
using System.Threading.Tasks;

namespace Access.Contract.Procedure
{
    public interface IProcedureAccess
    {
        Task<ProcedureAccessModel> CreateAsync(CreateProcedureAccessRequest accessRequest);
        Task<ProcedureAccessModel> CreateUserProcedureAsync(UpsertUserProcedureAccessRequest accessRequest);
        Task<ProcedureAccessModel> UpdateAsync(UpdateProcedureAccessRequest accessRequest);
        Task<ProcedureAccessModel> UpdateUserProcedureAsync(UpsertUserProcedureAccessRequest accessRequest);
        Task<IEnumerable<ProcedureAccessModel>> GetAllAsync();
        Task<ProcedureAccessModel> GetByIdAsync(string id, bool active);
        Task<IEnumerable<ProcedureAccessModel>> GetProceduresByUserIdAsync(string id);
        Task<ProcedureAccessModel> DeleteAsync(string id);
        Task<ProcedureAccessModel> DeleteUserProcedureAsync(string userId, string procedureId);
        Task<ProcedureAccessModel> RestoreAsync(string id);
        Task<bool> ValidateIdNameAsync(string name);
        Task<IEnumerable<string>> ValidateProcedureTeethAsync(IEnumerable<string> theetIds);
    }
}
