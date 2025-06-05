using Contract.Workspace.Orthodontics;
using Host.Api.Contract.Orthodontics;

namespace Host.Api.Contract.MapBuilders;

public interface IOrthodonticHostBuilder
{
    OrthodonticRequest MapOrthodonticApiRequestToOrthodonticRequest(OrthodonticApiRequest apiRequest, string id);
}
