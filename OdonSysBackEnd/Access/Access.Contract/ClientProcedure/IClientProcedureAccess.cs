namespace Access.Contract.ClientProcedure
{
    public interface IClientProcedureAccess
    {
        Task<IEnumerable<ClientProcedureAccessModel>> GetClientProceduresByUserClientIdAsync(IEnumerable<Guid> userClientIds);
        Task<ClientProcedureAccessModel> CreateClientProcedureAsync(CreateClientProcedureAccessRequest accessRequest);
        Task<ClientProcedureAccessModel> UpdateClientProcedureAsync(UpdateClientProcedureAccessRequest accessRequest);
        Task<bool> CheckExistsClientProcedureAsync(string userClientId, string procedureId);
        Task<bool> CheckExistsClientProcedureAsync(string clientProcedureId);
        Task<ClientProcedureAccessModel> GetClientProcedureByIdAsync(string clientProcedureId);
    }
}
