using Utilities.Enums;

namespace Contract.Workspace.Procedures
{
    public record UpdateClientProcedureRequest(
        string UserClientId,
        string ProcedureId,
        int Price,
        ProcedureStatus Status
    ) { }
}
