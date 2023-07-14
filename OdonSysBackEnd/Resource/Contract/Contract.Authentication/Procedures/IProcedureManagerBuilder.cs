using Access.Contract.ClientProcedures;
using Access.Contract.Procedures;
using Access.Contract.Users;
using Contract.Workspace.ClientProcedures;

namespace Contract.Workspace.Procedures
{
    public interface IProcedureManagerBuilder
    {
        CreateProcedureAccessRequest MapCreateProcedureRequestToCreateProcedureAccessRequest(CreateProcedureRequest createProcedureRequest);
        UpdateProcedureAccessRequest MapUpdateProcedureRequestToUpdateProcedureAccessRequest(UpdateProcedureRequest updateProcedureRequest);
        ProcedureModel MapProcedureAccessModelToProcedureModel(ProcedureAccessModel procedureAccessModel);
        UpdateProcedureRequest MapProcedureModelToUpdateProcedureRequest(ProcedureModel procedureModel);
        ClientProcedureModel MapClientProcedureAccessModelToClientProcedureModel(ClientProcedureAccessModel clientProcedureAccessModel);
        UserClientAccessRequest MapCreateClientProcedureRequestToUserClientAccessRequest(CreateClientProcedureRequest request);
        UpdateClientProcedureAccessRequest MapUpdateClientProcedureRequestToUpdateClientProcedureAccessRequest(UpdateClientProcedureRequest request);
    }
}
