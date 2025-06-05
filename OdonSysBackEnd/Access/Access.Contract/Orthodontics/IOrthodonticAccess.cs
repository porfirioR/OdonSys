namespace Access.Contract.Orthodontics;

public interface IOrthodonticAccess
{
    Task<OrthodonticAccessModel> GetByIdAsync(string id);
    Task<IEnumerable<OrthodonticAccessModel>> GetAllByClientIdAsync(string clientId);
    Task<IEnumerable<OrthodonticAccessModel>> GetAllAsync();
    Task<OrthodonticAccessModel> UpsertOrthodontic(OrthodonticAccessRequest accessRequest);
    Task<OrthodonticAccessModel> DeleteAsync(string id);
}