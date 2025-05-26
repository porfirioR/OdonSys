using Access.Contract.Clients;
using Access.Contract.Orthodontics;
using Contract.Workspace.Orthodontics;

namespace Manager.Workspace.Orthodontics;

internal class OrthodonticManager : IOrthodonticManager
{
    private readonly IOrthodonticAccess _orthodonticAccess;
    private readonly IOrthodonticManagerBuilder _orthodonticManagerBuilder;
    private readonly IClientDataAccessBuilder _clientDataAccessBuilder;

    public OrthodonticManager(IOrthodonticAccess orthodonticAccess, IOrthodonticManagerBuilder orthodonticManagerBuilder)
    {
        _orthodonticAccess = orthodonticAccess;
        _orthodonticManagerBuilder = orthodonticManagerBuilder;
    }

    public async Task<IEnumerable<OrthodonticModel>> GetAllAsync()
    {
        var accessModels = await _orthodonticAccess.GetAllAsync();
        var models = accessModels.Select(_orthodonticManagerBuilder.MapAccessModelToModel);
        return models;
    }

    public async Task<IEnumerable<OrthodonticModel>> GetAllByClientIdAsync(string clientId)
    {
        var accessModels = await _orthodonticAccess.GetAllByClientIdAsync(clientId);
        var models = accessModels.Select(_orthodonticManagerBuilder.MapAccessModelToModel);
        return models;
    }

    public async Task<OrthodonticModel> GetByIdAsync(string id)
    {
        var accessModel = await _orthodonticAccess.GetByIdAsync(id);
        var model = _orthodonticManagerBuilder.MapAccessModelToModel(accessModel);
        return model;
    }

    public async Task<OrthodonticModel> UpsertOrthodontic(OrthodonticRequest request)
    {
        var accessRequest = _orthodonticManagerBuilder.MapRequestToAccessRequest(request);
        var accessModel = await _orthodonticAccess.UpsertOrthodontic(accessRequest);
        var model = _orthodonticManagerBuilder.MapAccessModelToModel(accessModel);
        return model;
    }
}
