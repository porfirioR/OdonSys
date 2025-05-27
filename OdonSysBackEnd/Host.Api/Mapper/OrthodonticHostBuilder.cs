using Contract.Workspace.Orthodontics;
using Host.Api.Contract.MapBuilders;
using Host.Api.Contract.Orthodontics;

namespace Host.Api.Mapper;

internal sealed class OrthodonticHostBuilder : IOrthodonticHostBuilder
{
    public OrthodonticRequest MapOrthodonticApiRequestToOrthodonticRequest(OrthodonticApiRequest apiRequest, string id) => new(
        apiRequest.ClientId,
        apiRequest.Date,
        apiRequest.Description,
        id
    );
}
