using Access.Contract.Orthodontics;

namespace Contract.Workspace.Orthodontics;

public interface IOrthodonticManagerBuilder
{
    OrthodonticAccessRequest MapRequestToAccessRequest(OrthodonticRequest request);
    OrthodonticModel MapAccessModelToModel(OrthodonticAccessModel accessModel);
}