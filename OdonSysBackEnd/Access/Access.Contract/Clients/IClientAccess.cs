namespace Access.Contract.Clients
{
    public interface IClientAccess
    {
        Task<ClientAccessModel> CreateClientAsync(CreateClientAccessRequest accessRequest);
        Task<ClientAccessModel> UpdateClientAsync(UpdateClientAccessRequest accessRequest);
        Task<IEnumerable<ClientAccessModel>> GetAllAsync();
        Task<ClientAccessModel> GetByIdAsync(string id);
        Task<ClientAccessModel> GetByDocumentAsync(string document);
        Task<bool> IsDuplicateEmailAsync(string email, string id = null);
        Task<bool> IsDuplicateDocumentAsync(string document, string id);
        Task<IEnumerable<ClientAccessModel>> GetClientsByUserIdAsync(string id, string userName);
        Task<ClientAccessModel> DeleteAsync(string id);
        Task<IEnumerable<ClientAccessModel>> AssignClientToUserAsync(AssignClientAccessRequest accessRequest);
    }
}
