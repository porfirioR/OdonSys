using Access.Contract.Orthodontics;
using Contract.Administration.Clients;
using Contract.Workspace.Orthodontics;

namespace Manager.Workspace.Orthodontics;

internal sealed class OrthodonticManagerBuilder(IClientManagerBuilder clientManagerBuilder) : IOrthodonticManagerBuilder
{
    public IClientManagerBuilder _clientManagerBuilder { get; } = clientManagerBuilder;

    public OrthodonticModel MapAccessModelToModel(OrthodonticAccessModel accessModel)
    {
        var model = new OrthodonticModel(
            accessModel.Id,
            accessModel.Date,
            accessModel.Description,
            accessModel.DateCreated,
            accessModel.DateModified,
            _clientManagerBuilder.MapClientAccessModelToClientModel(accessModel.Client)
        );
        return model;
    }

    public OrthodonticAccessRequest MapRequestToAccessRequest(OrthodonticRequest request) => new(
        request.ClientId,
        request.Date,
        request.Description,
        request.Id
    );
}