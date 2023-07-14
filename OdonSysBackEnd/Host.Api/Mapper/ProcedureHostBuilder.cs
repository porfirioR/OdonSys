using Contract.Workspace.Procedures;
using Host.Api.Contract.MapBuilders;
using Host.Api.Contract.Procedures;

namespace Host.Api.Mapper
{
    internal sealed class ProcedureHostBuilder : IProcedureHostBuilder
    {
        public CreateProcedureRequest MapCreateProcedureApiRequestToCreateProcedureRequest(CreateProcedureApiRequest createProcedureApiRequest) => new(
            createProcedureApiRequest.Name,
            createProcedureApiRequest.Description,
            createProcedureApiRequest.Price,
            new List<string>(),
            createProcedureApiRequest.XRays
        );

        public UpdateProcedureRequest MapProcedureModelToUpdateProcedureRequest(ProcedureModel procedureModel) => new(
            procedureModel.Id,
            procedureModel.Description,
            procedureModel.Active,
            procedureModel.Price,
            procedureModel.ProcedureTeeth,
            procedureModel.XRays
        );

        public UpdateProcedureRequest MapUpdateProcedureApiRequestToUpdateProcedureRequest(UpdateProcedureApiRequest updateProcedureApiRequest) => new(
            updateProcedureApiRequest.Id,
            updateProcedureApiRequest.Description,
            updateProcedureApiRequest.Active,
            updateProcedureApiRequest.Price,
            updateProcedureApiRequest.ProcedureTeeth,
            updateProcedureApiRequest.XRays
        );
    }
}
