namespace Contract.Admin.Clients
{
    public interface IClientManager
    {
        Task<ClientModel> CreateAsync(CreateClientRequest request);
        Task<ClientModel> UpdateAsync(UpdateClientRequest request);
        Task<IEnumerable<ClientModel>> GetAllAsync();
        Task<ClientModel> GetByIdAsync(string id);
        Task<ClientModel> GetByDocumentAsync(string documentId);
        Task<IEnumerable<ClientModel>> GetClientsByUserIdAsync(string id, string userName);
        Task<ClientModel> DeleteAsync(string id);
        Task<IEnumerable<ClientModel>> AssignClientToUser(AssignClientRequest request);
    }
}
