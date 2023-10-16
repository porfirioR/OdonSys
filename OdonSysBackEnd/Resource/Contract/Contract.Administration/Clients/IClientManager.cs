using Contract.Administration.Reports;

namespace Contract.Administration.Clients
{
    public interface IClientManager
    {
        Task<ClientModel> CreateAsync(CreateClientRequest request);
        Task<ClientModel> UpdateAsync(UpdateClientRequest request);
        Task<IEnumerable<ClientModel>> GetAllAsync();
        Task<ClientModel> GetByIdAsync(string id);
        Task<ClientModel> GetByDocumentAsync(string documentId);
        Task<bool> IsDuplicateEmailAsync(string email, string id = null);
        Task<bool> IsDuplicateDocumentAsync(string document, string id);
        Task<IEnumerable<ClientModel>> GetClientsByUserIdAsync(string id, string userName, string externalId = "");
        Task<ClientReportModel> GetReportByIdAsync(string id);
        Task<ClientModel> DeleteAsync(string id);
        Task<IEnumerable<ClientModel>> AssignClientToUser(AssignClientRequest request);
    }
}
