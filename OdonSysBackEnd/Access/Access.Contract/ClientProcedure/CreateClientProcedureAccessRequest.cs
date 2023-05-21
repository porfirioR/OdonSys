namespace Access.Contract.ClientProcedure
{
    public record CreateClientProcedureAccessRequest(
        string UserClientId,
        string ProcedureId
    );
}
