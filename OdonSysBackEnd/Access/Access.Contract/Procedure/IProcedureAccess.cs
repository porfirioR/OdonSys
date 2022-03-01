using System.Collections.Generic;
using System.Threading.Tasks;

namespace Access.Contract.Procedure
{
    public interface IProcedureAccess
    {
        Task<ProcedureAccessResponse> CreateAsync(CreateProcedureAccessRequest accessRequest);
        Task<ProcedureAccessResponse> UpdateAsync(UpdateProcedureAccessRequest accessRequest);
        Task<IEnumerable<ProcedureAccessResponse>> GetAllAsync();
        Task<ProcedureAccessResponse> GetByIdAsync(string id, bool active);
        Task<ProcedureAccessResponse> DeleteAsync(string id);
        Task<ProcedureAccessResponse> RestoreAsync(string id);
    }
}
