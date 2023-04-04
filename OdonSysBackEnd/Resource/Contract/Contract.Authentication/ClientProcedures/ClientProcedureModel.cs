using Utilities.Enums;

namespace Contract.Workspace.ClientProcedures
{
    public record ClientProcedureModel(
        string ProcedureId,
        string UserClientId,
        ProcedureStatus Status,
        int Price,
        bool Anesthesia
    ) { }
}
