namespace Contract.Workspace.Orthodontics;

public interface IOrthodonticManager
{
    Task<OrthodonticModel> GetByIdAsync(string id);
    Task<IEnumerable<OrthodonticModel>> GetAllByClientIdAsync(string clientId);
    Task<IEnumerable<OrthodonticModel>> GetAllAsync();
    Task<OrthodonticModel> UpsertOrthodontic(OrthodonticRequest request);
}