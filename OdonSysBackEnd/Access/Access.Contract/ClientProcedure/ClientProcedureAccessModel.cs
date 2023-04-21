using Utilities.Enums;

namespace Access.Contract.ClientProcedure
{
    public record ClientProcedureAccessModel(
        string ProcedureId,
        string UserClientId,
        ProcedureStatus Status,
        int Price
    ) { }
}
