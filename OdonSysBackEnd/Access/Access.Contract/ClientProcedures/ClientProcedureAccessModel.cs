namespace Access.Contract.ClientProcedures
{
    public record ClientProcedureAccessModel(
        string Id,
        string ProcedureId,
        string UserClientId
    );
}
