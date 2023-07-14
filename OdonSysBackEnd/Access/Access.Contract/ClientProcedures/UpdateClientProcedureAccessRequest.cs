namespace Access.Contract.ClientProcedures
{
    public record UpdateClientProcedureAccessRequest(
        string UserClientId,
        string ProcedureId
    );
}
