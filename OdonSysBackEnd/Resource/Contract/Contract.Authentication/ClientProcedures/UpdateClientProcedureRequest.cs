namespace Contract.Workspace.ClientProcedures
{
    public record UpdateClientProcedureRequest(
        string UserClientId,
        string ProcedureId
    );
}
