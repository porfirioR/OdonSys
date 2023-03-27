﻿using Contract.Workspace.ClientProcedures;

namespace Contract.Workspace.Procedures
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

        Task<IEnumerable<ProcedureModel>> GetProceduresByUserIdAsync(string id);
        Task<ProcedureModel> CreateClientProcedureAsync(CreateClientProcedureRequest request);
        Task<bool> CheckExistsUserProcedureAsync(string userId, string procedureId);// check
        Task<ProcedureModel> UpdateClientProcedureAsync(UpdateClientProcedureRequest request);
    }
}
