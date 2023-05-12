namespace Access.Contract.ClientProcedure
{
    public record UpdateClientProcedureAccessRequest(
        string UserClientId,
        string ProcedureId
    );
}
