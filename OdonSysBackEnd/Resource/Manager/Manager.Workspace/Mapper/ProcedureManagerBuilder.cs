using Access.Contract.ClientProcedures;
using Access.Contract.Procedures;
using Access.Contract.Users;
using Contract.Workspace.ClientProcedures;
using Contract.Workspace.Procedures;

namespace Manager.Workspace.Mapper
{
    internal class ProcedureManagerBuilder : IProcedureManagerBuilder
    {
        public CreateProcedureAccessRequest MapCreateProcedureRequestToCreateProcedureAccessRequest(CreateProcedureRequest createProcedureRequest) => new(
            createProcedureRequest.Name,
            createProcedureRequest.Description,
            createProcedureRequest.ProcedureTeeth,
            createProcedureRequest.XRays,
            createProcedureRequest.Price
        );

        public ProcedureModel MapProcedureAccessModelToProcedureModel(ProcedureAccessModel procedureAccessModel) => new(
            procedureAccessModel.Id,
            procedureAccessModel.Active,
            procedureAccessModel.DateCreated,
            procedureAccessModel.DateModified,
            procedureAccessModel.Name,
            procedureAccessModel.Description,
            procedureAccessModel.Price,
            procedureAccessModel.ProcedureTeeth,
            procedureAccessModel.XRays
        );

        public UpdateProcedureRequest MapProcedureModelToUpdateProcedureRequest(ProcedureModel procedureModel) => new(
            procedureModel.Id,
            procedureModel.Description,
            procedureModel.Active,
            procedureModel.Price,
            procedureModel.ProcedureTeeth,
            procedureModel.XRays
        );

        public UpdateProcedureAccessRequest MapUpdateProcedureRequestToUpdateProcedureAccessRequest(UpdateProcedureRequest updateProcedureRequest)
        {
            var accessRequest = new UpdateProcedureAccessRequest(updateProcedureRequest.Id, updateProcedureRequest.Description, updateProcedureRequest.Active, updateProcedureRequest.Price, updateProcedureRequest.XRays)
            {
                ProcedureTeeth = updateProcedureRequest.ProcedureTeeth
            };
            return accessRequest;
        }

        public ClientProcedureModel MapClientProcedureAccessModelToClientProcedureModel(ClientProcedureAccessModel clientProcedureAccessModel) => new(
            clientProcedureAccessModel.Id,
            clientProcedureAccessModel.ProcedureId,
            clientProcedureAccessModel.UserClientId
        );

        public UserClientAccessRequest MapCreateClientProcedureRequestToUserClientAccessRequest(CreateClientProcedureRequest request) => new(
            request.ClientId,
            request.UserId
        );

        public UpdateClientProcedureAccessRequest MapUpdateClientProcedureRequestToUpdateClientProcedureAccessRequest(UpdateClientProcedureRequest request) => new(
            request.UserClientId,
            request.ProcedureId
        );
    }
}
