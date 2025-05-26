using Access.Contract.Orthodontics;

namespace Contract.Workspace.Orthodontics;

public interface IOrthodonticManager
{

    Task<OrthodonticAccessModel> GetByIdAsync(string id);
    Task<IEnumerable<OrthodonticAccessModel>> GetAllByClientIdAsync(string clientId);
    Task<IEnumerable<OrthodonticAccessModel>> GetAllAsync();
    Task<OrthodonticAccessModel> UpsertOrthodontic(OrthodonticAccessRequest accessRequest);
}
