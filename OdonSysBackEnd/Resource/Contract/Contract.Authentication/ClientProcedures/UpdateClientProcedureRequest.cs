using Utilities.Enums;

namespace Contract.Workspace.Procedures
{
    public record UpdateClientProcedureRequest(
        string UserId,
        string ProcedureId,
        int Price,
        ProcedureStatus Status
    )
    { }
}
