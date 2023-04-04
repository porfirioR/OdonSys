using Utilities.Enums;

namespace Access.Contract.ClientProcedure
{
    public record CreateClientProcedureAccessRequest(
        string UserClientId,
        string ProcedureId,
        int Price,
        bool Anesthesia,
        ProcedureStatus Status
    ) { }
}
