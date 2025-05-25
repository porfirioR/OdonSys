namespace Access.Contract.Orthodontics;

public interface IOrthodonticAccess
{
    Task<IEnumerable<OrthodonticAccessModel>> GetByIdAsync(string id);
    Task<OrthodonticAccessModel> GetAllByClientIdAsync(string clientId);
    Task<IEnumerable<OrthodonticAccessModel>> GetAllAsync();
    Task<OrthodonticAccessModel> UpsertOrthodontic(OrthodonticAccessRequest accessRequest);
}
