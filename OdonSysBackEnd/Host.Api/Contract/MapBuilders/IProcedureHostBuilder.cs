using Contract.Workspace.Procedures;
using Host.Api.Contract.Procedures;

namespace Host.Api.Contract.MapBuilders
{
    public interface IProcedureHostBuilder
    {
        CreateProcedureRequest MapCreateProcedureApiRequestToCreateProcedureRequest(CreateProcedureApiRequest createProcedureApiRequest);
        UpdateProcedureRequest MapProcedureModelToUpdateProcedureRequest(ProcedureModel response);
        UpdateProcedureRequest MapUpdateProcedureApiRequestToUpdateProcedureRequest(UpdateProcedureApiRequest updateProcedureApiRequest);
    }
}
