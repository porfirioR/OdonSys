﻿namespace Access.Contract.Procedures;

public interface IProcedureAccess
{
    Task<ProcedureAccessModel> CreateAsync(CreateProcedureAccessRequest accessRequest);
    Task<ProcedureAccessModel> UpdateAsync(UpdateProcedureAccessRequest accessRequest);
    Task<IEnumerable<ProcedureAccessModel>> GetAllAsync();
    Task<ProcedureAccessModel> GetByIdAsync(string id, bool active);
    Task<ProcedureAccessModel> DeleteAsync(string id);
    Task<bool> ValidateIdNameAsync(string name);
    //Task<IEnumerable<string>> ValidateProcedureTeethAsync(IEnumerable<string> theetIds);
}
