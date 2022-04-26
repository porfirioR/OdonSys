using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contract.Procedure.Procedures
{
    public interface IProcedureManager
    {
        Task<ProcedureModel> CreateAsync(CreateProcedureRequest request);
        Task<IEnumerable<ProcedureModel>> GetAllAsync();
        Task<ProcedureModel> GetByIdAsync(string id, bool active);
        Task<ProcedureModel> UpdateAsync(UpdateProcedureRequest request);
        Task<ProcedureModel> RestoreAsync(string id);
        Task<ProcedureModel> DeleteAsync(string id);
        Task<bool> ValidateIdNameAsync(string name);
        Task<IEnumerable<string>> ValidateProcedureTeethAsync(IEnumerable<string> ids);
    }
}
